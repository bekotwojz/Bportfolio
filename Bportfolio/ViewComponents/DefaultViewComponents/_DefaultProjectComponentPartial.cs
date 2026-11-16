using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bportfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Projects
                .Where(project => project.IsActive)
                .Include(project => project.Category)
                .ToList();
            return View(values);
            
        }
        
    }
}