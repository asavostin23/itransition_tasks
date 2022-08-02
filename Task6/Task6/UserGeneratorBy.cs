using Task6.Models;

namespace Task6
{
    public class UserGeneratorBy : AbstractUserGenerator
    {
        task6dbContext db;
        public override List<PersonViewModel> GetPeople(int page)
        {
            Random rand = new Random(Seed + page);
            List<PersonViewModel> people = new(10);
            for (int i = 0; i < 10; i++)
            {
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
                System.Text.StringBuilder adress = new();
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
                people.Add(new(surname ?? "undefined", name ?? "undefined", patronymic ?? "undefined", adress.ToString(), num));
            }
            return people;
        }
        public override List<PersonViewModel> GetFirstPage()
        {
            List<PersonViewModel> firstPage = GetPeople(0);
            firstPage.AddRange(GetPeople(1));
            return firstPage;
        }
        public UserGeneratorBy(task6dbContext db, int seed) : base(seed)
        {
            this.db = db;
        }
    }
}
