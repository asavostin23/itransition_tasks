using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task6.Models;

namespace Task6.Controllers
{
    public class CsvExportController : Controller
    {
        private readonly task6dbContext db;
        private AbstractUserGenerator userGenerator;
        private readonly IWebHostEnvironment _appEnvironment;
        public CsvExportController(task6dbContext db, IWebHostEnvironment appEnvironment)
        {
            this.db = db;
            _appEnvironment = appEnvironment;
        }
        public IActionResult Get(string region, int seed, int lastPage, float errorLevel)
        {
            switch (region)
            {
                case "pl":
                    userGenerator = new UserGeneratorPl(db, seed, errorLevel);
                    break;
                case "uk":
                    userGenerator = new UserGeneratorUk(db, seed, errorLevel);
                    break;
                default:
                    userGenerator = new UserGeneratorBy(db, seed, errorLevel);
                    break;
            }
            List<PersonViewModel> people = new List<PersonViewModel>();
            for (int i = 1; i<= lastPage; i++)
            {
                people.AddRange(userGenerator.GetPeople(i));
            }
            string virtualPath = Path.Combine(_appEnvironment.ContentRootPath, "Files\\temp.csv");
            using(StreamWriter streamWriter = new StreamWriter(virtualPath, false))
            {
                CsvConfiguration csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
                csvConfig.Delimiter = ";";
                using(CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfig))
                {
                    csvWriter.WriteRecords(people);
                }
            }
            return PhysicalFile(virtualPath, "text/csv", "temp.csv");
        }
    }
}
