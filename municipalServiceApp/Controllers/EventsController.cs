using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace municipalServiceApp.Controllers
{
    public class EventsController : Controller
    {
        //SortedDictionary (POE Required)
        private static SortedDictionary<int, EventModel> eventsData = new SortedDictionary<int, EventModel>();

        private static HashSet<string> categories = new HashSet<string>();

        // Queue (POE Required)
        private static Queue<EventModel> newEventQueue = new Queue<EventModel>();

        // Recently viewed Stack (POE Required)
        private static Stack<EventModel> recentlyViewedStack = new Stack<EventModel>();

        // Important events Priority Queue (POE Required)
        private static PriorityQueue<EventModel, int> priorityEvents = new PriorityQueue<EventModel, int>();

        // search history Dictionary (POE Required)
        private static Dictionary<string, int> searchHistory = new Dictionary<string, int>();

     
        private void TrackSearch(string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                //finding what the user searched for within categories
                foreach (var category in categories)
                {
                    if (category.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        if (searchHistory.ContainsKey(category))
                            searchHistory[category]++;
                        else
                            searchHistory[category] = 1;
                    }
                }
            }
        }

        // (POE Required: Additional Recommendation Feature)
        // THIS IS FOR RECOMMENDED EVENTS
        private List<EventModel> GetRecommendedEvents()
        {
            if (!searchHistory.Any())
                return new List<EventModel>();

            var topCategory = searchHistory.OrderByDescending(kvp => kvp.Value).First().Key;

            return eventsData.Values
                .Where(e => e.Category.Contains(topCategory, StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.Date)
                .Take(5)
                .ToList();
        }
       // THIS IS FOR RECOMMENDED EVENTS END 

        static EventsController()
        {
            // STATIC EVENT DATA
            var sampleEvents = new List<EventModel>
            {
                new EventModel(1, "Autumn Street Cleanup", "Environmental", new DateTime(2025, 10, 18), "Central Park"),
                new EventModel(2, "City Library Book Drive", "Education", new DateTime(2025, 10, 22), "Main Library"),
                new EventModel(3, "Community Health Check", "Health", new DateTime(2025, 10, 28), "Civic Center Hall A"),
                new EventModel(4, "Food Waste Awareness Workshop", "Sustainability", new DateTime(2025, 11, 2), "Greenfield Community Center"),
                new EventModel(5, "Fire Safety Drill", "Public Safety", new DateTime(2025, 11, 6), "Eastside Fire Station"),
                new EventModel(6, "Public Art Walk", "Culture", new DateTime(2025, 10, 25), "Old Town Square"),
                new EventModel(7, "Tree Planting Day", "Environmental", new DateTime(2025, 11, 9), "Riverside Park"),
                new EventModel(8, "Senior Citizens Fitness Class", "Health", new DateTime(2025, 10, 30), "Harmony Recreation Center"),
                new EventModel(9, "Water Conservation Seminar", "Utilities", new DateTime(2025, 11, 12), "City Hall, Conference Room B"),
                new EventModel(10, "Neighborhood Watch Meeting", "Public Safety", new DateTime(2025, 11, 15), "Maple Street Community Hall"),
                new EventModel(11, "Holiday Lights Prep", "Community Engagement", new DateTime(2025, 10, 20), "City Square Plaza"),
                new EventModel(13, "Road Safety Awareness Campaign", "Public Safety", new DateTime(2025, 11, 22), "Main Street Intersection"),
                new EventModel(14, "Composting Workshop", "Sustainability", new DateTime(2025, 10, 27), "Greenfield Community Garden"),
                new EventModel(15, "Volunteer Day at Animal Shelter", "Community Engagement", new DateTime(2025, 11, 5), "City Animal Shelter")
            };

            foreach (var e in sampleEvents)
            {
                eventsData[e.Id] = e;
                categories.Add(e.Category);

                if (e.Category == "Safety" || e.Category == "Health")
                    priorityEvents.Enqueue(e, 1);
                else
                    priorityEvents.Enqueue(e, 5);
            }
        }

        //EVENTS 
        public IActionResult Index(string search = "", string sort = "date", DateTime? date = null)
        {
            var filteredEvents = eventsData.Values.AsEnumerable();

            // SEARCHING FEATURE
            if (!string.IsNullOrWhiteSpace(search))
            {
                filteredEvents = filteredEvents.Where(e =>
                    e.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    e.Category.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            // DATE SORTING FEATURE
            if (date.HasValue)
            {
                filteredEvents = filteredEvents.Where(e => e.Date.Date == date.Value.Date);
            }

            // CATEGORY SORTING FEATURE
            filteredEvents = sort switch
            {
                "name" => filteredEvents.OrderBy(e => e.Name),
                "category" => filteredEvents.OrderBy(e => e.Category),
                _ => filteredEvents.OrderBy(e => e.Date)
            };


            // (POE Required: Additional Recommendation Feature)
            //ALGORITHM FEATURE
            //REFERENCE: https://stackoverflow.com/questions/10521831/how-to-display-a-list-using-viewbag
            TrackSearch(search);
            ViewBag.RecommendedEvents = GetRecommendedEvents();

            ViewBag.Categories = categories;
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.SelectedDate = date;

            return View(filteredEvents.ToList());
        }


        public IActionResult ViewEvent(int id)
        {
            if (eventsData.ContainsKey(id))
            {
                var ev = eventsData[id];
                recentlyViewedStack.Push(ev);
                return View(ev);
            }

            return NotFound();
        }

        public IActionResult RecentEvents()
        {
            return View(recentlyViewedStack.ToList());
        }

        [HttpPost]
        public IActionResult AddEvent(string name, string category, DateTime date, string location, bool urgent = false)
        {
            int newId = eventsData.Keys.Max() + 1;
            var newEvent = new EventModel(newId, name, category, date, location);

            // GETS ADDED TO MAIN STORAGE
            eventsData[newId] = newEvent;
            categories.Add(category);

            newEventQueue.Enqueue(newEvent);
            priorityEvents.Enqueue(newEvent, urgent ? 1 : 5);

            return RedirectToAction("Index");
        }
    }


    public class EventModel
    {
        public EventModel(int id, string name, string category, DateTime date, string location)
        {
            Id = id;
            Name = name;
            Category = category;
            Date = date;
            Location = location;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
