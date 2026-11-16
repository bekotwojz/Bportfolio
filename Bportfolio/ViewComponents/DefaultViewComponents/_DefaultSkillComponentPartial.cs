using Bportfolio.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bportfolio.ViewComponents.DefaultViewComponents;

public class _DefaultSkillComponentPartial : ViewComponent
{
    private readonly PortfolioContext _context;

    public _DefaultSkillComponentPartial(PortfolioContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var skills = _context.Skills
            .AsNoTracking()
            .Include(skill => skill.SkillItems)
            .OrderBy(skill => skill.Title)
            .ToList();

        return View(skills);
    }
}
