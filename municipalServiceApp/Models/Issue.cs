namespace municipalServiceApp.Models
{
    public enum IssuePriority
    {
        High,
        Medium,
        Low
    }

    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? MediaFilePath { get; set; }
        public string Status { get; set; } = "Received"; // Received, In Progress, Resolved
        public IssuePriority Priority { get; set; }
        public DateTime DateReported { get; set; } = DateTime.Now;
    }
}
