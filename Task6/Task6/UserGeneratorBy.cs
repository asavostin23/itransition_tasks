using Task6.Models;

namespace Task6
{
    public class UserGeneratorBy : AbstractUserGenerator
    {
        public override List<PersonViewModel> GetPeople(int page)
        {
            Random rand = new Random(Seed + page);
            List<PersonViewModel> people = new(10);
            PersonViewModel tempPerson;
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
                SettlementBy settlement;
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
                adress.Append($"{settlement.Type} {settlement.Name}");
                if ((num & 8) > 0)
                {
                    adress.Append($" , {db.StreetsBy.Skip(num % db.StreetsBy.Count()).First().Name}");
                    adress.Append($" , кв. {(num % 300) + 1}");
                }
                adress.Append($", д. {(num % 100) + 1}");
                if ((num & 16) > 0)
                    adress.Append($", корп. {(num % 20) + 1}");
                string phone = "+375 " + (num % 4) switch
                {
                    0 => " (29) ",
                    1 => " (25) ",
                    2 => " (33) ",
                    3 => " (44) "
                };
                phone += (num % 10000000).ToString();
                tempPerson = new PersonViewModel(num, surname ?? "undefined", name ?? "undefined", patronymic ?? "undefined", adress.ToString(), phone);
                AbstractPersonErrorAdder personErrorAdder = new PersonErrorAdderBy(ErrorLevel);
                personErrorAdder.AddErrors(tempPerson);
                people.Add(tempPerson);
            }
            return people;
        }
        public UserGeneratorBy(task6dbContext db, int seed, float errorLevel) : base(db, seed, errorLevel)
        { }
    }
}
