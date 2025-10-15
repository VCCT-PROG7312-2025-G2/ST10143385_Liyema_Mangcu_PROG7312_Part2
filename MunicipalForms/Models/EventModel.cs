using System;

namespace MunicipalForms.Models
{
    public class EventModel
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        public EventModel(string title, string category, string description, DateTime date, string location)
        {
            Title = title;
            Category = category;
            Description = description;
            Date = date;
            Location = location;
        }
    }
}
