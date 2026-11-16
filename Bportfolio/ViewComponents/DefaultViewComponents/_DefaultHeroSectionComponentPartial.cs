using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Bportfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultHeroSectionComponentPartial (PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var value = _context.HeroSections.FirstOrDefault();
            if (value == null) return View();
            ViewBag.greeting = value.Greeting;
            ViewBag.title = value.Title;
            ViewBag.Description = value.Description;
            ViewBag.MainImageUrl = value.MainImageUrl;
            return View();
        }
    }
}