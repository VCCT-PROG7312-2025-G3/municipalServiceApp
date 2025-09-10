using System.Collections.Concurrent;
using municipalServiceApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace municipalServiceApp.Services
{
    public static class IssueQueueService
    {
        private static readonly ConcurrentQueue<Issue> HighPriority = new();
        private static readonly ConcurrentQueue<Issue> MediumPriority = new();
        private static readonly ConcurrentQueue<Issue> LowPriority = new();

        public static void Enqueue(Issue issue)
        {
            switch (issue.Priority)
            {
                case IssuePriority.High:
                    HighPriority.Enqueue(issue);
                    break;
                case IssuePriority.Medium:
                    MediumPriority.Enqueue(issue);
                    break;
                default:
                    LowPriority.Enqueue(issue);
                    break;
            }
        }

        public static IEnumerable<Issue> GetAll()
        {
            return HighPriority.Concat(MediumPriority).Concat(LowPriority);
        }
    }
}
