using Microsoft.AspNetCore.Mvc;
using MunicipalForms.Data;
using MunicipalForms.Models;

namespace MunicipalForms.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ServiceRepository _repo;

        public ServiceController(ServiceRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var requests = _repo.GetAll();
            return View(requests);
        }

        public IActionResult All() => View();

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }
    }
}
