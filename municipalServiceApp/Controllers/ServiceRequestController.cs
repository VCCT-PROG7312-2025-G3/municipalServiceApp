using Microsoft.AspNetCore.Mvc;
using municipalServiceApp.Models;
using municipalServiceApp.Services;
using System.Linq;

//NEEDED FOR PART 3 OF POE
namespace municipalServiceApp.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly RequestStatusService _statusService;

        public ServiceRequestController(RequestStatusService statusService)
        {
            _statusService = statusService;
        }

        // Display all service requests
        public IActionResult Index()
        {
            var requests = _statusService.Requests; 
            return View(requests);
        }

        public IActionResult Details(int id)
        {
            var request = _statusService.Requests.FirstOrDefault(r => r.RequestId == id);
            if (request == null) return NotFound();
            return View(request);
        }
    }
}
