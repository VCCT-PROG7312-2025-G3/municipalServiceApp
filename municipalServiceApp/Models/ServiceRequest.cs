//NEEDED FOR PART 3 OF POE
using System;
using municipalServiceApp.Models;

namespace municipalServiceApp.Models 
{ 
    public class ServiceRequest
    {
        public int RequestId { get; set; }
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public IssuePriority Priority { get; set; }
        public RequestStatus Status { get; set; }
    }
}
