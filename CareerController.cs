
using Microsoft.AspNetCore.Mvc;
using CareerRecommendationSystem.Models;
using CareerRecommendationSystem.Services;

namespace CareerRecommendationSystem.Controllers
{
    public class CareerController : Controller
    {
        private readonly CareerRecommendationService _service;

        public CareerController(
            CareerRecommendationService service)
        {
            _service = service;
        }

        // Open Career Assessment Page
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CareerAssessment());
        }

        // Submit Assessment Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Recommend(CareerAssessment student)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", student);
            }

            var result = new CareerResultViewModel
            {
                Student = student,
                Recommendations = _service.Recommend(student)
            };

            return View("Results", result);
        }
    }
}
