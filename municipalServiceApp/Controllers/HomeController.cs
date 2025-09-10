using Microsoft.AspNetCore.Mvc;
using municipalServiceApp.Models;
using municipalServiceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace municipalServiceApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home page
        public IActionResult Index()
        {
            return View();
        }

        // GET: Feedback / Updates
        public IActionResult FeedbackUpdates(string searchCategory, string searchLocation, DateTime? searchDate)
        {
            var issues = IssueQueueService.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchCategory))
                issues = issues.Where(i => i.Category == searchCategory);

            if (!string.IsNullOrEmpty(searchLocation))
                issues = issues.Where(i => i.Location.Contains(searchLocation));

            if (searchDate.HasValue)
                issues = issues.Where(i => i.DateReported.Date == searchDate.Value.Date);

            var filteredIssues = issues
                .OrderBy(i => i.Priority)
                .ThenBy(i => i.DateReported)
                .ToList();

            // Optional: for filter dropdowns in the view
            ViewBag.Categories = new List<string> { "Pothole", "Streetlight", "Water Leak", "Garbage" };

            return View(filteredIssues);
        }

        // Optional: Privacy page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
