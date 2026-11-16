using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Bportfolio.Context;
using Bportfolio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Bportfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            ViewBag.categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            var values = _context.Experiences.ToList();
            return View(values);
        }

        public IActionResult EditExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult EditExperience(Experience model, int id)
        {
            var edititem = _context.Experiences.Find(id);
            edititem.Title = model.Title;
            edititem.CompanyName = model.CompanyName;
            edititem.Description = model.Description;
            edititem.CategoryName = model.CategoryName;
            edititem.IsActive = model.IsActive;

            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }

        [HttpPost]
        public IActionResult CreateExperience(Experience model, int? categoryId)
        {
            if (!categoryId.HasValue)
            {
                return BadRequest("Kategori seçimi zorunludur.");
            }

            var category = _context.Categories.Find(categoryId.Value);
            if (category == null)
            {
                return BadRequest("Seçilen kategori bulunamadı.");
            }

            model.CategoryName = category.CategoryName;
            _context.Experiences.Add(model);
            _context.SaveChanges();

            return RedirectToAction("ExperienceList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteExperience(int id)
        {
            var experience = _context.Experiences.Find(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experiences.Remove(experience);
            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }
    }
}