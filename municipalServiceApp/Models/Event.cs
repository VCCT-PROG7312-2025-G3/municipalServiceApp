using System;

namespace municipalServiceApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Organizer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
