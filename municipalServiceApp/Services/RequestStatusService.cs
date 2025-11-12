//NEEDED FOR PART 3 OF POE
using municipalServiceApp.DataStructures;
using municipalServiceApp.Models;
using System;
using System.Collections.Generic;

namespace municipalServiceApp.Services
{ 
    public class RequestStatusService
    {
        public List<ServiceRequest> Requests { get; } = new();

        public RequestStatusService()
        {
            // Hardcoded initial requests
            Requests.Add(new ServiceRequest
            {
                RequestId = 1,
                RequestTitle = "Water Leak",
                Description = "Burst pipe near the park",
                DateSubmitted = DateTime.Now.AddDays(-2),
                Priority = IssuePriority.High,
                Status = RequestStatus.InProgress
            });

            Requests.Add(new ServiceRequest
            {
                RequestId = 2,
                RequestTitle = "Street Light Out",
                Description = "Lamp post not working",
                DateSubmitted = DateTime.Now.AddDays(-1),
                Priority = IssuePriority.Medium,
                Status = RequestStatus.Submitted
            });

            Requests.Add(new ServiceRequest
            {
                RequestId = 3,
                RequestTitle = "Garbage Pickup Delay",
                Description = "No collection this week",
                DateSubmitted = DateTime.Now.AddDays(-3),
                Priority = IssuePriority.Low,
                Status = RequestStatus.Completed
            });
        }

        public BinarySearchTree<int> RequestTree { get; set; } = new();
        public AVLTree<int> RequestAVL { get; set; } = new();
        public RedBlackTree<int> IdRB { get; set; } = new();
        public Dictionary<int, List<Edge>> RequestGraph { get; set; } = new();

        public void AddRequest(ServiceRequest request)
        {
            request.RequestId = Requests.Count > 0 ? Requests[^1].RequestId + 1 : 1;

            Requests.Add(request);

            // Add ID to trees and graphs
            RequestTree.Insert(request.RequestId);
            RequestAVL.Insert(request.RequestId);
            IdRB.Insert(request.RequestId);
            if (!RequestGraph.ContainsKey(request.RequestId))
                RequestGraph[request.RequestId] = new List<Edge>();
        }      

        public void ClearAll()
        {
            Requests.Clear();
            RequestTree = new BinarySearchTree<int>();
            RequestAVL = new AVLTree<int>();
            IdRB = new RedBlackTree<int>();
            RequestGraph.Clear();
        }
    }
}
