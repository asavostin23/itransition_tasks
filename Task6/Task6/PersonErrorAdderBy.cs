using System.Text;
using Task6.Models;

namespace Task6
{
    public class PersonErrorAdderBy : AbstractPersonErrorAdder
    {

        protected readonly char[] symbols = { 'а', 'б', 'в', 'г', 'д', 'e', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        protected override PersonViewModel Person { get; set; }
        public PersonErrorAdderBy(PersonViewModel person, float errorLevel) : base(person, errorLevel)
        {

        }
        public override PersonViewModel AddErrors()
        {
            int errorCount = (int)Math.Truncate(errorLevel);
            if (Person.UniqueId % 100 < (errorLevel - Math.Truncate(errorLevel)) * 100)
                errorCount++;
            for (int i = 0; i < errorCount; i++)
            {
                switch ((Person.UniqueId + i) % 3)
                {
                    case 0:
                        AddSymbolRemovingError(i);
                        break;
                    case 1:
                        AddRandomSymbolError(i);
                        break;
                    case 2:
                        AddSwapSymbolError(i);
                        break;
                }
            }
            return Person;
        }
        protected void AddSymbolRemovingError(int step)
        {
            if ((Person.Name.Length * Person.Surname.Length * Person.Patronymic.Length * Person.Adress.Length * Person.Phone.Length) == 0)
                return;
            switch ((Person.UniqueId + step + 1) % 5)
            {
                case 0:
                    Person.Name = Person.Name.Remove((Person.UniqueId + step) % Person.Name.Length, 1);
                    break;
                case 1:
                    Person.Surname = Person.Surname.Remove((Person.UniqueId + step) % Person.Surname.Length, 1);
                    break;
                case 2:
                    Person.Patronymic = Person.Patronymic.Remove((Person.UniqueId + step) % Person.Patronymic.Length, 1);
                    break;
                case 3:
                    Person.Adress = Person.Adress.Remove((Person.UniqueId + step) % Person.Adress.Length, 1);
                    break;
                case 4:
                    Person.Phone = Person.Phone.Remove((Person.UniqueId + step) % Person.Phone.Length, 1);
                    break;
            };
        }
        protected void AddRandomSymbolError(int step)
        {
            if ((Person.Name.Length * Person.Surname.Length * Person.Patronymic.Length * Person.Adress.Length * Person.Phone.Length) == 0)
                return;
            switch ((Person.UniqueId + step + 2) % 5)
            {
                case 0:
                    Person.Name = Person.Name.Insert((Person.UniqueId + step) % Person.Name.Length, symbols[(Person.UniqueId + step) % 43].ToString());
                    break;
                case 1:
                    Person.Surname = Person.Surname.Insert((Person.UniqueId + step) % Person.Surname.Length, symbols[(Person.UniqueId + step) % 43].ToString());
                    break;
                case 2:
                    Person.Patronymic = Person.Patronymic.Insert((Person.UniqueId + step) % Person.Patronymic.Length, symbols[(Person.UniqueId + step) % 43].ToString());
                    break;
                case 3:
                    Person.Adress = Person.Adress.Insert((Person.UniqueId + step) % Person.Adress.Length, symbols[(Person.UniqueId + step) % 43].ToString());
                    break;
                case 4:
                    Person.Phone = Person.Phone.Insert((Person.UniqueId + step) % Person.Phone.Length, symbols[(Person.UniqueId + step) % 43].ToString());
                    break;
            };
        }
        protected void AddSwapSymbolError(int step)
        {
            if ((Person.Name.Length * Person.Surname.Length * Person.Patronymic.Length * Person.Adress.Length * Person.Phone.Length) == 0)
                return;
            string data;
            switch ((Person.UniqueId + step + 3) % 5)
            {
                case 0:
                    data = Person.Name;
                    break;
                case 1:
                    data = Person.Surname;
                    break;
                case 2:
                    data = Person.Patronymic;
                    break;
                case 3:
                    data = Person.Adress;
                    break;
                case 4:
                    data = Person.Phone;
                    break;
                default:
                    data = Person.Name;
                    break;
            }
            int firstCharNum = (Person.UniqueId + step) % data.Length;
            int secondCharNum = (Person.UniqueId + data.Length / 2 + step * 2) % data.Length;
            StringBuilder tempData = new(data);
            tempData[firstCharNum] = data[secondCharNum];
            tempData[secondCharNum] = data[firstCharNum];
            switch ((Person.UniqueId + step + 3) % 5)
            {
                case 0:
                    Person.Name = tempData.ToString();
                    break;
                case 1:
                    Person.Surname = tempData.ToString();
                    break;
                case 2:
                    Person.Patronymic = tempData.ToString();
                    break;
                case 3:
                    Person.Adress = tempData.ToString();
                    break;
                case 4:
                    Person.Phone = tempData.ToString();
                    break;
            }
        }
    }
}
