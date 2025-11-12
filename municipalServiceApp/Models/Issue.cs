namespace municipalServiceApp.Models
{
    public enum IssuePriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public enum RequestStatus
    {
        Submitted,
        InProgress,
        Completed,
        Rejected
    }
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? MediaFilePath { get; set; }
        public IssuePriority Priority { get; set; }

        public string Status { get; set; } = "Received"; // Received, In Progress, Resolved
        public DateTime DateReported { get; set; } = DateTime.Now;
    }
}
