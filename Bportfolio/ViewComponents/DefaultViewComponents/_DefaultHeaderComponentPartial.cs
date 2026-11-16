using Microsoft.AspNetCore.Mvc;

namespace Bportfolio.ViewComponents.DefaultComponents;

public class _DefaultHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}