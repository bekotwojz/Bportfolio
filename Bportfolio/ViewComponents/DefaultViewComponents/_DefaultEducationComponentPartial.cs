using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Microsoft.AspNetCore.Mvc;

namespace Bportfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultEducationComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultEducationComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Educations
                .Where(education => education.IsActive)
                .OrderByDescending(education => education.EducationId)
                .ToList();
            return View(values);
        }
    }
}