using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Task6.Models;

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
            int seed = 0;/////////////
            AbstractUserGenerator userGenerator = new UserGeneratorBy(db, seed);
            return View(userGenerator.GetFirstPage());
        }

    }
}