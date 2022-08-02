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
            Random rand = new Random();
            int num = rand.Next();
            bool isMale = num % 2 == 0;
            string? surname, name;
            surname = db.SurnamesBy
                .Where(x => x.IsMale == isMale)
                .Skip(num % db.SurnamesBy.Where(x => x.IsMale == isMale).Count())
                .First().Name;
            name = db.NamesBy
                .Where(x => x.IsMale == isMale)
                .Skip(num % db.NamesBy.Where(x => x.IsMale == isMale).Count())
                .First().Name;
            Settlement settlement = db.SettlementsBy.Skip(num % db.SettlementsBy.Count()).First();
            StringBuilder adress = new();
            if (settlement.Type == "г.")
            {
                if ((num & 2) > 0)
                    adress.Append($"{settlement.Region}, ");
                if ((num & 4) > 0)
                    adress.Append($"{settlement.District}, ");
            }
            adress.Append($"{settlement.Type} {settlement.Name}");
            PersonViewModel person = new(surname ?? "undefined", name ?? "undefined", adress.ToString(), num);
            return View(person);
        }

    }
}