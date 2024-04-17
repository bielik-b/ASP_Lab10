using ASP_Lab10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_Lab10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Consultation consultation)
        {
            if (consultation.Subject.Equals("Basics") && consultation.Date.DayOfWeek == DayOfWeek.Monday)
                ModelState.AddModelError("", "Consultation on \"Basics\" can't take place on Mondays!");
            if (consultation.Date <= DateTime.Now)
                ModelState.AddModelError("Date", "Date should be set in the future!");
            if (consultation.Date.DayOfWeek == DayOfWeek.Sunday || consultation.Date.DayOfWeek == DayOfWeek.Saturday)
                ModelState.AddModelError("Date", "Date can't be set on weekends!");
            if (ModelState.IsValid)
                return View("Confirm", consultation);
            else
            {
                foreach (var item in ModelState)
                {
                    if (item.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                        foreach (var error in item.Value.Errors)
                            consultation.Errors.Add($"{error.ErrorMessage}");
                }
                return View(consultation);
            }
        }
        [HttpGet]
        public IActionResult Subjects()
        {
            List<Subject> subjects = new List<Subject>();
            subjects.Add(new Subject("JavaScript", "JQuery, Node.js, Vue.js"));
            subjects.Add(new Subject("C#", ".NET Framework, ASP.NET Core MVC, Razor pages"));
            subjects.Add(new Subject("Java", "JavaFX, Jakarta EE, Spring"));
            subjects.Add(new Subject("Python", "Big Data, Django"));
            subjects.Add(new Subject("Basics", "OOP, patterns, databases"));
            return View(subjects);
        }
        [HttpGet]
        public IActionResult Confirm(Consultation consultation)
        {
            return View(consultation);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

