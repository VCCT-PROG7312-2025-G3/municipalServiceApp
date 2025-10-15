using System;
using System.Collections.Generic;
using System.Linq;
using municipalServiceApp.Models;

namespace municipalServiceApp.Services
{
    public static class EventService
    {
        // SortedDictionary (POE Required)
        private static readonly SortedDictionary<int, Event> _events = new();

        // Queue (POE Required)
        private static readonly Queue<Event> _submissionQueue = new();

        // HashSet (POE Required)
        private static readonly HashSet<string> _categories = new();

        //STATIC EVENT DATA
        static EventService()
        {
            SeedData();
        }

        private static void SeedData()
        {
            var sampleEvents = new List<Event>
            {
                new() { Id = 1, Title = "Autumn Street Cleanup", Category = "Environmental", Description = "Help clean and beautify city streets", Location = "Central Park", Date = new DateTime(2025, 10, 18), Organizer = "City Council" },
                new() { Id = 2, Title = "City Library Book Drive", Category = "Education", Description = "Donate and exchange books for all ages", Location = "Main Library", Date = new DateTime(2025, 10, 22), Organizer = "Library Association" },
                new() { Id = 3, Title = "Community Health Check", Category = "Health", Description = "Free health screenings and wellness advice", Location = "Civic Center Hall A", Date = new DateTime(2025, 10, 28), Organizer = "Health Dept" },
                new() { Id = 4, Title = "Food Waste Awareness Workshop", Category = "Sustainability", Description = "Learn how to reduce food waste at home", Location = "Greenfield Community Center", Date = new DateTime(2025, 11, 2), Organizer = "Green Living SA" },
                new() { Id = 5, Title = "Fire Safety Drill", Category = "Public Safety", Description = "Practice fire safety and emergency procedures", Location = "Eastside Fire Station", Date = new DateTime(2025, 11, 6), Organizer = "Fire Dept" },
                new() { Id = 6, Title = "Public Art Walk", Category = "Culture", Description = "Guided walk showcasing public artworks", Location = "Old Town Square", Date = new DateTime(2025, 10, 25), Organizer = "Arts Council" },
                new() { Id = 7, Title = "Tree Planting Day", Category = "Environmental", Description = "Join the community to plant trees", Location = "Riverside Park", Date = new DateTime(2025, 11, 9), Organizer = "Environmental Affairs" },
                new() { Id = 8, Title = "Senior Citizens Fitness Class", Category = "Health", Description = "Light exercises for senior wellbeing", Location = "Harmony Recreation Center", Date = new DateTime(2025, 10, 30), Organizer = "Recreation Dept" },
                new() { Id = 9, Title = "Water Conservation Seminar", Category = "Utilities", Description = "Tips for saving water in your home", Location = "City Hall, Conference Room B", Date = new DateTime(2025, 11, 12), Organizer = "Utilities Dept" },
                new() { Id = 10, Title = "Neighborhood Watch Meeting", Category = "Public Safety", Description = "Discuss local safety initiatives", Location = "Maple Street Community Hall", Date = new DateTime(2025, 11, 15), Organizer = "Community Police Forum" },
                new() { Id = 11, Title = "Holiday Lights Prep", Category = "Community Engagement", Description = "Help decorate the city for the holidays", Location = "City Square Plaza", Date = new DateTime(2025, 10, 20), Organizer = "City Council" },
                new() { Id = 12, Title = "Career Expo", Category = "Education", Description = "Explore career opportunities and meet employers", Location = "Expo Centre", Date = new DateTime(2025, 11, 19), Organizer = "HR Network" },
                new() { Id = 13, Title = "Road Safety Awareness Campaign", Category = "Public Safety", Description = "Promoting safe driving habits", Location = "Main Street Intersection", Date = new DateTime(2025, 11, 22), Organizer = "Traffic Dept" },
                new() { Id = 14, Title = "Composting Workshop", Category = "Sustainability", Description = "Learn to compost organic waste", Location = "Greenfield Community Garden", Date = new DateTime(2025, 10, 27), Organizer = "Green Living SA" },
                new() { Id = 15, Title = "Volunteer Day at Animal Shelter", Category = "Community Engagement", Description = "Assist with animal care and adoption support", Location = "City Animal Shelter", Date = new DateTime(2025, 11, 5), Organizer = "Animal Rescue SA" }
            };

            foreach (var e in sampleEvents)
            {
                _events[e.Id] = e;
                _categories.Add(e.Category);
            }
        }

        public static IEnumerable<Event> GetAll() => _events.Values;

        public static IEnumerable<string> GetCategories() => _categories;

        public static void AddEvent(Event newEvent)
        {
            newEvent.Id = _events.Keys.Max() + 1;
            _submissionQueue.Enqueue(newEvent);
            _events[newEvent.Id] = newEvent;
            _categories.Add(newEvent.Category);
        }

        public static Queue<Event> GetSubmissionQueue() => _submissionQueue;
    }
}
