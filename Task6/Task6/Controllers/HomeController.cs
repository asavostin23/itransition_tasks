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
            string? surname, name, patronymic;
            surname = db.SurnamesBy
                .Where(x => x.IsMale == isMale)
                .Skip(num % db.SurnamesBy.Where(x => x.IsMale == isMale).Count())
                .First().Name;
            name = db.NamesBy
                .Where(x => x.IsMale == isMale)
                .Skip(num % db.NamesBy.Where(x => x.IsMale == isMale).Count())
                .First().Name;
            patronymic = db.PatronymicsBy
                .Where(x => x.IsMale == isMale)
                .Skip(num % db.PatronymicsBy.Where(x => x.IsMale == isMale).Count())
                .First().Name;

            Settlement settlement;
            StringBuilder adress = new();
            if ((num & 8) > 0)
            {
                settlement = db.SettlementsBy
                    .Where(settlement => settlement.Type == "г.")
                    .Skip(num % db.SettlementsBy.Where(settlement => settlement.Type == "г.").Count())
                    .First();
                if ((num & 2) > 0)
                    adress.Append($"{settlement.Region}, ");
                if ((num & 4) > 0 && settlement.District != null)
                    adress.Append($"{settlement.District}, ");
            }
            else
            {
                settlement = db.SettlementsBy
                    .Where(settlement => settlement.Type != "г.")
                    .Skip(num % db.SettlementsBy.Where(settlement => settlement.Type != "г.").Count())
                    .First();
            }
            adress.Append($"{settlement.Type} {settlement.Name}, д. {num % 100}");
            if ((num & 8) > 0)
                adress.Append($" , кв. {num % 300}");
            PersonViewModel person = new(surname ?? "undefined", name ?? "undefined", patronymic ?? "undefined", adress.ToString(), num);
            return View(person);
        }

    }
}