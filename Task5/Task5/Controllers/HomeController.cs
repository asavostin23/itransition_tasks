using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Task5.Hubs;
using Task5.Models;

namespace Task5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected ApplicationContext db;
        protected IHubContext<MessageHub> hubContext;
        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext, IHubContext<MessageHub> hubContext)
        {
            _logger = logger;
            db = applicationContext;
            this.hubContext = hubContext;
        }
        [HttpGet]
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
    }
}