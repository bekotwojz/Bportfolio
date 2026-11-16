using Bportfolio.Context;
using Bportfolio.Entities;
using Bportfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bportfolio.Controllers;

public class DefaultController : Controller
{
    private readonly PortfolioContext _context;

    public DefaultController(PortfolioContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SendMessage(ContactMessageInputModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ContactFormStatus"] = "error";
            return Redirect("/#contact");
        }

        var message = new Message
        {
            SenderName = model.SenderName.Trim(),
            SenderEmail = model.SenderEmail.Trim(),
            Subject = model.Subject?.Trim() ?? string.Empty,
            MessageDetail = model.MessageDetail.Trim(),
            SendDate = DateTime.Now,
            IsRead = false
        };

        _context.Messages.Add(message);
        _context.SaveChanges();
        TempData["ContactFormStatus"] = "success";

        return Redirect("/#contact");
    }
}