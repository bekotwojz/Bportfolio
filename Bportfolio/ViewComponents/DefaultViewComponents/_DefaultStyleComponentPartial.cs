using Microsoft.AspNetCore.Mvc;

namespace Bportfolio.ViewComponents.DefaultComponents
{
    public class _DefaultStyleComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}