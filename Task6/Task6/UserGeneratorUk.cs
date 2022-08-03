using Task6.Models;

namespace Task6
{
    public class UserGeneratorUk : AbstractUserGenerator
    {
        public UserGeneratorUk(task6dbContext db, int seed, float errorLevel) : base(db, seed, errorLevel)
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
                surname = db.SurnamesUk
                    .Skip(num % db.SurnamesUk.Count())
                    .First().Name;
                name = db.NamesUk
                    .Where(x => x.IsMale == isMale)
                    .Skip(num % db.NamesUk.Where(x => x.IsMale == isMale).Count())
                    .First().Name;
                SettlementUk settlement;
                System.Text.StringBuilder adress = new();

                adress.Append($" {(num % 100) + 1}");
                if ((num & 8) > 0)
                {
                    adress.Append($" {db.StreetsUk.Skip(num % db.StreetsUk.Count()).First().Name}");
                    adress.Append($" {(num % 300) + 1}");
                }
                settlement = db.SettlementsUk
                        .Skip(num % db.SettlementsUk.Count())
                        .First();
                adress.Append($" {settlement.Name}");

                string phone = "+447 " + (num % 1000000000).ToString();
                tempPerson = new PersonViewModel(num, surname ?? "undefined", name ?? "undefined", "", adress.ToString(), phone);
                AbstractPersonErrorAdder personErrorAdder = new PersonErrorAdderUk(ErrorLevel);
                personErrorAdder.AddErrors(tempPerson);
                people.Add(tempPerson);
            }
            return people;
        }
    }
}
