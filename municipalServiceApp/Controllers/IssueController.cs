using Microsoft.AspNetCore.Mvc;
using municipalServiceApp.Models;
using municipalServiceApp.Services;
using System.IO;

namespace municipalServiceApp.Controllers
{
    public class IssueController : Controller
    {
        private readonly RequestStatusService _requestService;

        public IssueController(RequestStatusService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public IActionResult ReportIssue()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportIssue(Issue issue, IFormFile mediaFile)
        {
            if (string.IsNullOrWhiteSpace(issue.Location))
                ModelState.AddModelError("Location", "Location is required.");
            if (string.IsNullOrWhiteSpace(issue.Category))
                ModelState.AddModelError("Category", "Please select a category.");
            if (string.IsNullOrWhiteSpace(issue.Description))
                ModelState.AddModelError("Description", "Description cannot be empty.");

            if (!ModelState.IsValid)
                return View(issue);

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

            issue.Priority = IssuePriority.Medium;
            issue.Status = "Received";
            issue.DateReported = System.DateTime.Now;

            // Add as ServiceRequest
            var serviceRequest = new ServiceRequest
            {
                RequestTitle = issue.Category,
                Description = issue.Description,
                DateSubmitted = System.DateTime.Now,
                Priority = issue.Priority,
                Status = RequestStatus.Submitted
            };

            _requestService.AddRequest(serviceRequest);

            TempData["ShowThankYou"] = true;
            return RedirectToAction("Index", "Home");
        }
    }
}
