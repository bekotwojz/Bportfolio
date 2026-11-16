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
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _environment;

        public AboutController(PortfolioContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult AboutMe()
        {
            var values = _context.HeroSections.ToList();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAboutMe(HeroSection model, int id, IFormFile? image)
        {
            var edititem = _context.HeroSections.Find(id);
            if (edititem == null)
                return NotFound();

            if (image != null)
            {
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                if (image.Length == 0 || image.Length > 5 * 1024 * 1024 || !allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("image", "JPG, PNG veya WebP biçiminde, en fazla 5 MB boyutunda bir görsel seçin.");
                    model.HeroSectionId = id;
                    model.MainImageUrl = edititem.MainImageUrl;
                    return View("AboutMe", new List<HeroSection> { model });
                }

                using var input = image.OpenReadStream();
                var header = new byte[12];
                var bytesRead = await input.ReadAtLeastAsync(header, header.Length, throwOnEndOfStream: false);
                var detectedExtension = bytesRead switch
                {
                    >= 3 when header[0] == 0xff && header[1] == 0xd8 && header[2] == 0xff => ".jpg",
                    >= 8 when header.AsSpan(0, 8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }) => ".png",
                    >= 12 when header.AsSpan(0, 4).SequenceEqual("RIFF"u8) && header.AsSpan(8, 4).SequenceEqual("WEBP"u8) => ".webp",
                    _ => null
                };
                if (detectedExtension == null)
                {
                    ModelState.AddModelError("image", "Dosya içeriği desteklenen JPG, PNG veya WebP biçiminde değil.");
                    model.HeroSectionId = id;
                    model.MainImageUrl = edititem.MainImageUrl;
                    return View("AboutMe", new List<HeroSection> { model });
                }

                var imageDirectory = Path.Combine(_environment.WebRootPath, "img");
                Directory.CreateDirectory(imageDirectory);
                var fileName = $"{Guid.NewGuid():N}{detectedExtension}";
                input.Position = 0;
                await using var output = new FileStream(Path.Combine(imageDirectory, fileName), FileMode.CreateNew);
                await input.CopyToAsync(output);
                edititem.MainImageUrl = fileName;
            }

            edititem.Greeting = model.Greeting;
            edititem.Title = model.Title;
            edititem.Description = model.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("AboutMe");
        }
    }
}