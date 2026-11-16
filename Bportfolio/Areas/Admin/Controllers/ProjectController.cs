using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Bportfolio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bportfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;

        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ProjectList()
        {
            ViewBag.projectCount = _context.Projects.Count();
            ViewBag.categoryCount = _context.Categories.Count();
            var values = _context.Projects.Include(project => project.Category).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult EditProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
                return NotFound();

            SetCategories(project.CategoryId);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProject(Project model)
        {
            if (!_context.Categories.Any(category => category.CategoryId == model.CategoryId))
                ModelState.AddModelError(nameof(model.CategoryId), "Geçerli bir kategori seçin.");

            if (!ModelState.IsValid)
            {
                SetCategories(model.CategoryId);
                return View(model);
            }

            var project = _context.Projects.Find(model.ProjectId);
            if (project == null)
                return NotFound();

            project.ProjectTitle = model.ProjectTitle.Trim();
            project.Description = model.Description.Trim();
            project.CategoryId = model.CategoryId;
            project.IsActive = model.IsActive;
            project.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl)
                ? null
                : System.IO.Path.GetFileName(model.ImageUrl.Trim().Replace('\\', '/'));

            _context.SaveChanges();
            return RedirectToAction(nameof(ProjectList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction(nameof(ProjectList));
        }

        private void SetCategories(int selectedCategoryId)
        {
            ViewBag.categories = new SelectList(
                _context.Categories.OrderBy(category => category.CategoryName).ToList(),
                "CategoryId",
                "CategoryName",
                selectedCategoryId);
        }
    }
}