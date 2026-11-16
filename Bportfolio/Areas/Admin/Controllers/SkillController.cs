using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Bportfolio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bportfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillList()
        {
            var values = _context.Skills.Include(skill => skill.SkillItems).OrderBy(skill => skill.Title).ToList();
            return View(values);
        }

        public IActionResult EditSkill(int id)
        {
            var skill = _context.Skills.Include(item => item.SkillItems).FirstOrDefault(item => item.SkillId == id);
            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSkill(Skill model, int id)
        {
            model.SkillItems ??= new List<SkillItem>();
            model.Description ??= string.Empty;
            model.IconUrl ??= string.Empty;
            ModelState.Remove(nameof(Skill.SkillItems));
            ModelState.Remove(nameof(Skill.Description));
            ModelState.Remove(nameof(Skill.IconUrl));

            for (var index = 0; index < model.SkillItems.Count; index++)
            {
                ModelState.Remove($"SkillItems[{index}].Skill");
                ModelState.Remove($"SkillItems[{index}].SkillId");
                if (string.IsNullOrWhiteSpace(model.SkillItems[index].SkillItemTitle))
                    ModelState.Remove($"SkillItems[{index}].SkillItemTitle");
            }

            model.SkillItems = model.SkillItems
                .Where(item => !string.IsNullOrWhiteSpace(item.SkillItemTitle))
                .ToList();

            if (!ModelState.IsValid)
                return View(model);

            var skill = _context.Skills.Include(item => item.SkillItems).FirstOrDefault(item => item.SkillId == id);
            if (skill == null)
                return NotFound();

            skill.Title = model.Title;
            skill.Description = model.Description;
            skill.IconUrl = model.IconUrl;

            var submittedIds = model.SkillItems
                .Where(item => item.SkillItemId > 0)
                .Select(item => item.SkillItemId)
                .ToHashSet();
            var removedItems = skill.SkillItems.Where(item => !submittedIds.Contains(item.SkillItemId)).ToList();
            _context.SkillItems.RemoveRange(removedItems);

            foreach (var submittedItem in model.SkillItems)
            {
                var existingItem = skill.SkillItems.FirstOrDefault(item => item.SkillItemId == submittedItem.SkillItemId);
                if (existingItem == null)
                {
                    skill.SkillItems.Add(new SkillItem
                    {
                        SkillItemTitle = submittedItem.SkillItemTitle,
                        SkillId = skill.SkillId
                    });
                }
                else
                {
                    existingItem.SkillItemTitle = submittedItem.SkillItemTitle;
                }
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(SkillList));
        }
    }
}