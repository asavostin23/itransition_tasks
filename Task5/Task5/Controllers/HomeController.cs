using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task5.Models;

namespace Task5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected ApplicationContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            db = applicationContext;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("name");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string name)
        {
            HttpContext.Session.SetString("name", name);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult WriteMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> WriteMessage(string title, string body, string reciever)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                db.Messages.Add(new Message(title, body, HttpContext.Session.GetString("name"), reciever));
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}