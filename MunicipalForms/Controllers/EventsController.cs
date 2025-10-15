using Microsoft.AspNetCore.Mvc;
using MunicipalForms.Models;
using System;
using System.Linq;

namespace MunicipalForms.Controllers
{
    public class EventsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string category, DateTime? date)
        {
            var events = EventData.SearchEvents(category, date);
            ViewBag.Categories = EventData.GetCategories();
            ViewBag.Recommendations = EventData.GetRecommendations();
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedDate = date?.ToString("yyyy-MM-dd");
            return View(events);
        }
    }
}
