using Microsoft.AspNetCore.Mvc;
using MunicipalForms.Models;
using System.IO;

namespace MunicipalForms.Controllers
{
    public class ReportController : Controller
    {
        // Linked list for all the users submitted reports
        public IActionResult Index()
        {
            var reports = ReportLinkedList.GetReports();
            return View(reports);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string location, string category, string description, IFormFile AttachmentPath)
        {
            try
            {
                string mediaPath = null;

                if (AttachmentPath != null && AttachmentPath.Length > 0)
                {
                    Console.WriteLine($"Uploading file: {AttachmentPath.FileName}, Size: {AttachmentPath.Length} bytes");

                    // Some validation for the user's attachment: 5MB limit
                    if (AttachmentPath.Length > 5 * 1024 * 1024)
                    {
                        Console.WriteLine("File too large, max 5MB.");
                        return Json(new { success = false, message = "File size exceeds 5MB limit." });
                    }

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(AttachmentPath.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        AttachmentPath.CopyTo(stream);
                    }

                    mediaPath = "/Uploads/" + fileName;
                    Console.WriteLine($"File saved to: {mediaPath}");
                }

                var report = new Report
                {
                    Location = location,
                    Category = category,
                    Description = description,
                    AttachmentPath = mediaPath,
                    SubmittedAt = DateTime.Now
                };

                ReportLinkedList.AddReport(report);
                Console.WriteLine($"Report created: {location}, {category}");

       
                return Json(new
                {
                    success = true,
                    file = mediaPath,
                    location = report.Location,
                    category = report.Category,
                    description = report.Description,
                    submittedAt = report.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}