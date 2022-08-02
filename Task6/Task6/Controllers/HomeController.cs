using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Task6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly task6dbContext db;
        public HomeController(ILogger<HomeController> logger, task6dbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}