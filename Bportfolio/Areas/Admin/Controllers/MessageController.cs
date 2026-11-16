using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bportfolio.Context;
using Bportfolio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bportfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList()
        {
            var values = _context.Messages.OrderByDescending(message => message.SendDate).ToList();
            return View(values);
        }

        public IActionResult MessageDetail(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
                return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            var messageDetails = new
            {
                message.MessageId,
                message.SenderName,
                message.SenderEmail,
                message.Subject,
                message.MessageDetail,
                SendDate = message.SendDate.ToString("dd MMM yyyy, HH:mm"),
                message.IsRead
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(messageDetails);

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleRead(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
                return NotFound();

            message.IsRead = !message.IsRead;
            _context.SaveChanges();
            return Json(new { message.IsRead });
        }

        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMessage(Message model)
        {
            model.SendDate = DateTime.Now;
            model.IsRead = false;
            _context.Messages.Add(model);
            _context.SaveChanges();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMessage(int id)
        {
            var delitem = _context.Messages.Find(id);
            if (delitem == null) return NotFound();
            _context.Messages.Remove(delitem);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
    }
}