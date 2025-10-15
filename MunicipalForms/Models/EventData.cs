using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalForms.Models
{
    public static class EventData
    {
        // SortedDictionary
        private static SortedDictionary<DateTime, List<EventModel>> eventsByDate = new SortedDictionary<DateTime, List<EventModel>>();

        // HashSet 
        private static HashSet<string> categories = new HashSet<string>();

        // Queue 
        private static Queue<string> recentSearches = new Queue<string>();

        // Stack
        private static Stack<EventModel> recentlyViewed = new Stack<EventModel>();

        // Example Data
        static EventData()
        {
            // local event data 
            AddEvent(new EventModel("Community Clean-Up", "Community", "Join your neighbors for a cleanup!", DateTime.Now.AddDays(3), "City Park"));
            AddEvent(new EventModel("Neighborhood Watch", "Community", "Discuss safety updates and tips with local officers.", DateTime.Now.AddDays(8), "Town Hall"));
            AddEvent(new EventModel("Charity Fun Run", "Community", "Participate in a 5K run to support local shelters.", DateTime.Now.AddDays(18), "Riverside Park"));

            AddEvent(new EventModel("Health Fair", "Health", "Free health screenings and wellness tips.", DateTime.Now.AddDays(7), "Community Hall"));
            AddEvent(new EventModel("Blood Donation Drive", "Health", "Donate blood and help save lives.", DateTime.Now.AddDays(11), "Red Cross Center"));
            AddEvent(new EventModel("Mental Health Talk", "Health", "Join professionals for an open discussion on mental wellness.", DateTime.Now.AddDays(21), "Public Library Auditorium"));

            AddEvent(new EventModel("Soccer Tournament", "Sports", "Join or cheer for local teams!", DateTime.Now.AddDays(14), "Local Stadium"));
            AddEvent(new EventModel("Cycling for a Cause", "Sports", "Ride for charity through scenic routes.", DateTime.Now.AddDays(25), "Greenway Trail"));

            AddEvent(new EventModel("Food Festival", "Culture", "Taste foods from around the world!", DateTime.Now.AddDays(10), "Town Square"));
            AddEvent(new EventModel("Art Exhibition", "Culture", "Explore art pieces by local talent.", DateTime.Now.AddDays(16), "Civic Center"));
            AddEvent(new EventModel("Outdoor Movie Night", "Culture", "Enjoy a classic film under the stars.", DateTime.Now.AddDays(22), "Central Park"));

            AddEvent(new EventModel("Tree Planting Drive", "Environment", "Help us plant 100 new trees around the city.", DateTime.Now.AddDays(12), "Eastside Park"));
            AddEvent(new EventModel("Beach Cleanup", "Environment", "Join the community in cleaning up the coastline.", DateTime.Now.AddDays(17), "Bayfront Beach"));
            AddEvent(new EventModel("Recycling Workshop", "Environment", "Learn how to recycle effectively and reduce waste.", DateTime.Now.AddDays(27), "Eco Center"));
        }

        public static void AddEvent(EventModel ev)
        {
            if (!eventsByDate.ContainsKey(ev.Date))
                eventsByDate[ev.Date] = new List<EventModel>();

            eventsByDate[ev.Date].Add(ev);
            categories.Add(ev.Category);
        }

        public static IEnumerable<EventModel> GetAllEvents()
        {
            return eventsByDate.SelectMany(kvp => kvp.Value);
        }

        public static IEnumerable<EventModel> SearchEvents(string category, DateTime? date)
        {
            var allEvents = GetAllEvents();

            if (!string.IsNullOrEmpty(category))
            {
                allEvents = allEvents.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
                if (recentSearches.Count >= 5)
                    recentSearches.Dequeue();
                recentSearches.Enqueue(category);
            }

            if (date.HasValue)
            {
                allEvents = allEvents.Where(e => e.Date.Date == date.Value.Date);
            }

            return allEvents;
        }

        public static IEnumerable<EventModel> GetRecommendations()
        {
            var popular = recentSearches
                .GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(2);

            return GetAllEvents().Where(e => popular.Contains(e.Category));
        }

        public static IEnumerable<string> GetCategories() => categories;
    }
}
