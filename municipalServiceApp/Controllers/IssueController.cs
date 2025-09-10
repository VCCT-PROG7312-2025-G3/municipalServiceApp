using Microsoft.AspNetCore.Mvc;
using municipalServiceApp.Models;
using municipalServiceApp.Services;
using System.IO;

namespace municipalServiceApp.Controllers
{
    public class IssueController : Controller
    {
        // GET: Show report form
        [HttpGet]
        public IActionResult ReportIssue()
        {
            return View();
        }

        // POST: Handle form submission
        [HttpPost]
        public IActionResult ReportIssue(Issue issue, IFormFile mediaFile)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(issue.Location))
                ModelState.AddModelError("Location", "Location is required.");

            if (string.IsNullOrWhiteSpace(issue.Category))
                ModelState.AddModelError("Category", "Please select a category.");

            if (string.IsNullOrWhiteSpace(issue.Description))
                ModelState.AddModelError("Description", "Description cannot be empty.");

            // Return form if invalid
            if (!ModelState.IsValid)
                return View(issue);

            // Handle optional media upload
            if (mediaFile != null && mediaFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, mediaFile.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                mediaFile.CopyTo(stream);

                issue.MediaFilePath = "/uploads/" + mediaFile.FileName;
            }

            // Set defaults
            issue.Priority = IssuePriority.Medium;
            issue.Status = "Received";
            issue.DateReported = System.DateTime.Now;

            // Enqueue the issue
            IssueQueueService.Enqueue(issue);

            // Set TempData for thank-you message/modal
            TempData["ShowThankYou"] = true;

            // Always redirect to home
            return RedirectToAction("Index", "Home");
        }
    }
}
