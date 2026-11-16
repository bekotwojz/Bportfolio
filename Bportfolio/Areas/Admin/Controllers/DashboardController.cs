using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Bportfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bportfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;
        public DashboardController (PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalProjects = _context.Projects.Count(),
                ActiveProjects = _context.Projects.Count(project => project.IsActive),
                SkillGroups = _context.Skills.Count(),
                TechnologyLevels = new List<TechnologyLevel>
                {
                    new() { Name = "C#", Percentage = 80 },
                    new() { Name = "HTML", Percentage = 85 },
                    new() { Name = "CSS", Percentage = 75 },
                    new() { Name = "Bootstrap", Percentage = 80 },
                    new() { Name = "Tailwind", Percentage = 65 },
                    new() { Name = "JavaScript", Percentage = 75 },
                    new() { Name = "TypeScript", Percentage = 60 },
                    new() { Name = "SQL", Percentage = 75 }
                },
                Experiences = _context.Experiences.Count(),
                Educations = _context.Educations.Count(),
                TotalMessages = _context.Messages.Count(),
                UnreadMessages = _context.Messages.Count(message => !message.IsRead),
                RecentMessages = _context.Messages
                    .AsNoTracking()
                    .OrderByDescending(message => message.SendDate)
                    .Take(5)
                    .ToList(),
                RecentProjects = _context.Projects
                    .AsNoTracking()
                    .Include(project => project.Category)
                    .OrderByDescending(project => project.ProjectId)
                    .Take(3)
                    .ToList(),
                CategorySummaries = _context.Projects
                    .AsNoTracking()
                    .GroupBy(project => project.Category.CategoryName)
                    .Select(group => new CategoryProjectSummary
                    {
                        CategoryName = group.Key,
                        ProjectCount = group.Count()
                    })
                    .OrderByDescending(category => category.ProjectCount)
                    .ThenBy(category => category.CategoryName)
                    .ToList()
            };

            return View(model);
        }
    }
}