using Task6.Models;

namespace Task6
{
    public class UserGeneratorPl : AbstractUserGenerator
    {
        public UserGeneratorPl(task6dbContext db, int seed, float errorLevel) : base(db, seed, errorLevel)
        { }
        public override List<PersonViewModel> GetPeople(int page)
        {
            Random rand = new Random(Seed + page);
            List<PersonViewModel> people = new(10);
            PersonViewModel tempPerson;
            for (int i = 0; i < 10; i++)
            {
                int num = rand.Next();
                bool isMale = num % 2 == 0;
                string? surname, name;
                surname = db.SurnamesPl
                    .Where(x => x.IsMale == isMale)
                    .Skip(num % db.SurnamesPl.Where(x => x.IsMale == isMale).Count())
                    .First().Name;
                name = db.NamesPl
                    .Where(x => x.IsMale == isMale)
                    .Skip(num % db.NamesPl.Where(x => x.IsMale == isMale).Count())
                    .First().Name;
                SettlementPl settlement;
                System.Text.StringBuilder adress = new();
                if ((num & 8) > 0)
                {
                    settlement = db.SettlementsPl
                        .Where(settlement => settlement.Type == "m.")
                        .Skip(num % db.SettlementsPl.Where(settlement => settlement.Type == "m.").Count())
                        .First();
                    adress.Append($"{db.StreetsPl.Skip(num % db.StreetsBy.Count()).First().Name}");
                }
                else
                {
                    settlement = db.SettlementsPl
                        .Where(settlement => settlement.Type != "m.")
                        .Skip(num % db.SettlementsPl.Where(settlement => settlement.Type != "m.").Count())
                        .First();
                }
                adress.Append($" {(num % 100) + 1}");
                if ((num & 8) > 0)
                {
                    adress.Append($"/{(num % 300) + 1}");
                }
                adress.Append($", {settlement.Type} {settlement.Name}");
                string[] mobileCode = new string[] { " (532) ", " (538) ", " (539) ", " (692) ", " (696) ", " (698) ", " (797) ", " (450) ", " (697) ", " (50) ", " (51) ", " (7361) ", " (7362) ", " (7292) " };
                string phone = "+48 " + mobileCode[num % mobileCode.Length];
                phone += (num % 10000000).ToString();
                tempPerson = new PersonViewModel(num, surname ?? "undefined", name ?? "undefined", "", adress.ToString(), phone);
                AbstractPersonErrorAdder personErrorAdder = new PersonErrorAdderPl(ErrorLevel);
                personErrorAdder.AddErrors(tempPerson);
                people.Add(tempPerson);
            }
            return people;
        }
    }
}
