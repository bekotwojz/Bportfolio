using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bportfolio.Areas.Admin.ViewComponents;

public class _AdminLayoutSidebarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Areas/Admin/Views/Shared/Components/_AdminLayoutSidebarComponentPartial/Default.cshtml");
    }
}
