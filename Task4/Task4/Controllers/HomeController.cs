using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Task4.Models;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected ApplicationContext db { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationContext db)
        {
            _logger = logger;
            this.db = db;
        }
        public async Task<bool> IsUserBlockedAsync()
        {
            User? loginUser = await db.Users.FirstOrDefaultAsync(u => u.Name == HttpContext.User.Identity.Name);
            return loginUser == null || loginUser.IsBlocked;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (await IsUserBlockedAsync())
                return RedirectToAction("Blocked");
            return View(await db.Users.ToListAsync());
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
        public async Task<IActionResult> Login(string inputEmail, string inputPassword)
        {
            if (string.IsNullOrEmpty(inputEmail) || string.IsNullOrEmpty(inputPassword))
            {
                ViewBag.WrongPassword = "is-invalid";
                return View();
            }
            if (db.Users.Select(u => u.Email).Any(em => em == inputEmail))
            {
                User? user = await db.Users.FirstOrDefaultAsync(u => u.Email == inputEmail && u.Password == inputPassword);
                if (user != null)
                {
                    List<Claim> claims = new() { new Claim(ClaimTypes.Name, user.Name) };
                    ClaimsIdentity claimsIdentity = new(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    user.LastLoginDate = DateTime.Now;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.WrongPassword = "is-invalid";
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string inputName, string inputEmail, string inputPassword)
        {
            if (string.IsNullOrEmpty(inputEmail) || string.IsNullOrEmpty(inputPassword)
                || db.Users.Select(u => u.Email).Contains(inputEmail) || db.Users.Select(u => u.Name).Contains(inputName))
            {
                ViewBag.Error = "Register error";
                return View();
            }
            User user = new(inputName, inputEmail, inputPassword);
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Blocked() => View();
        [Authorize]
        public async Task<IActionResult> Block(int[] userId)
        {
            if (await IsUserBlockedAsync())
                return RedirectToAction("Blocked");
            foreach (int id in userId)
            {
                if (db.Users.Select(u => u.Id).Contains(id))
                {
                    User user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                    user.IsBlocked = true;
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Unblock(int[] userId)
        {
            if (await IsUserBlockedAsync())
                return RedirectToAction("Blocked");
            foreach (int id in userId)
            {
                if (db.Users.Select(u => u.Id).Contains(id))
                {
                    User user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                    user.IsBlocked = false;
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Delete(int[] userId)
        {
            if (await IsUserBlockedAsync())
                return RedirectToAction("Blocked");
            foreach (int id in userId)
            {
                if (db.Users.Select(u => u.Id).Contains(id))
                {
                    User user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User != null)
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}