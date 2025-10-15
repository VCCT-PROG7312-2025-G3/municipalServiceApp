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
        
        public IActionResult Index()
        {
            return View();
        }

        //GOES TO FEEDBACK TAB
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

            ViewBag.Categories = new List<string> { "Pothole", "Streetlight", "Water Leak", "Garbage" };

            return View(filteredIssues);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
