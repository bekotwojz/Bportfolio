using Microsoft.AspNetCore.Mvc;

namespace Bportfolio.ViewComponents.DefaultViewComponents;

public class _DefaultNavbarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}