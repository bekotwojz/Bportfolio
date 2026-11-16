using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Bportfolio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bportfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult EducationList()
        {
            var values = _context.Educations.OrderBy(item => item.Program).ToList();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEducation(Education model)
        {
            model.Description ??= string.Empty;
            model.Status ??= string.Empty;
            ModelState.Remove(nameof(Education.Description));
            ModelState.Remove(nameof(Education.Status));

            if (!ModelState.IsValid)
            {
                ViewBag.OpenEducationModal = model.EducationId == 0 ? "add" : "edit";
                ViewBag.FormEducation = model;
                return View("EducationList", _context.Educations.OrderBy(item => item.Program).ToList());
            }

            if (model.EducationId == 0)
            {
                _context.Educations.Add(model);
            }
            else
            {
                var education = _context.Educations.Find(model.EducationId);
                if (education == null)
                    return NotFound();

                education.Program = model.Program;
                education.InstitutionName = model.InstitutionName;
                education.Description = model.Description;
                education.Status = model.Status;
                education.IsActive = model.IsActive;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(EducationList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEducation(int id)
        {
            var education = _context.Educations.Find(id);
            if (education == null)
                return NotFound();

            _context.Educations.Remove(education);
            _context.SaveChanges();
            return RedirectToAction(nameof(EducationList));
        }
    }
}