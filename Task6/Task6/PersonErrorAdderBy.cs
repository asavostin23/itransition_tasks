using System.Text;
using Task6.Models;

namespace Task6
{
    public class PersonErrorAdderBy : AbstractPersonErrorAdder
    {

        protected readonly char[] symbols = { 'а', 'б', 'в', 'г', 'д', 'e', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public PersonErrorAdderBy(float errorLevel) : base(errorLevel)
        {}
        public override PersonViewModel AddErrors(PersonViewModel person)
        {
            int errorCount = (int)Math.Truncate(errorLevel);
            if (person.UniqueId % 100 < (errorLevel - Math.Truncate(errorLevel)) * 100)
                errorCount++;
            for (int i = 0; i < errorCount; i++)
            {
                switch ((person.UniqueId + i) % 3)
                {
                    case 0:
                        AddSymbolRemovingError(person, i);
                        break;
                    case 1:
                        AddRandomSymbolError(person, i);
                        break;
                    case 2:
                        AddSwapSymbolError(person, i);
                        break;
                }
            }
            return person;
        }

        protected void AddRandomSymbolError(PersonViewModel person, int step)
        {
            if ((person.Name.Length * person.Surname.Length * person.Patronymic.Length * person.Adress.Length * person.Phone.Length) == 0)
                return;
            switch ((person.UniqueId + step + 2) % 5)
            {
                case 0:
                    person.Name = person.Name.Insert((person.UniqueId + step) % person.Name.Length, symbols[(person.UniqueId + step) % 43].ToString());
                    break;
                case 1:
                    person.Surname = person.Surname.Insert((person.UniqueId + step) % person.Surname.Length, symbols[(person.UniqueId + step) % 43].ToString());
                    break;
                case 2:
                    person.Patronymic = person.Patronymic.Insert((person.UniqueId + step) % person.Patronymic.Length, symbols[(person.UniqueId + step) % 43].ToString());
                    break;
                case 3:
                    person.Adress = person.Adress.Insert((person.UniqueId + step) % person.Adress.Length, symbols[(person.UniqueId + step) % 43].ToString());
                    break;
                case 4:
                    person.Phone = person.Phone.Insert((person.UniqueId + step) % person.Phone.Length, symbols[(person.UniqueId + step) % 43].ToString());
                    break;
            };
        }

    }
}
