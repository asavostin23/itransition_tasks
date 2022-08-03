using System.Text;
using Task6.Models;

namespace Task6
{
    public class PersonErrorAdderBy : AbstractPersonErrorAdder
    {
        public PersonErrorAdderBy(float errorLevel) : base(errorLevel)
        {
            symbols = new char[]{ 'а', 'б', 'в', 'г', 'д', 'e', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        }
        protected override void AddSwapSymbolError(PersonViewModel person, int step)
        {
            if ((person.Name.Length * person.Surname.Length * person.Patronymic.Length * person.Adress.Length * person.Phone.Length) == 0)
                return;
            string data;
            switch ((person.UniqueId + step + 3) % 5)
            {
                case 0:
                    data = person.Name;
                    break;
                case 1:
                    data = person.Surname;
                    break;
                case 2:
                    data = person.Patronymic;
                    break;
                case 3:
                    data = person.Adress;
                    break;
                case 4:
                    data = person.Phone;
                    break;
                default:
                    data = person.Name;
                    break;
            }
            int firstCharNum = (person.UniqueId + step) % data.Length;
            int secondCharNum = (person.UniqueId + data.Length / 2 + step * 2) % data.Length;
            StringBuilder tempData = new(data);
            tempData[firstCharNum] = data[secondCharNum];
            tempData[secondCharNum] = data[firstCharNum];
            switch ((person.UniqueId + step + 3) % 5)
            {
                case 0:
                    person.Name = tempData.ToString();
                    break;
                case 1:
                    person.Surname = tempData.ToString();
                    break;
                case 2:
                    person.Patronymic = tempData.ToString();
                    break;
                case 3:
                    person.Adress = tempData.ToString();
                    break;
                case 4:
                    person.Phone = tempData.ToString();
                    break;
            }
        }
        protected override void AddSymbolRemovingError(PersonViewModel person, int step)
        {
            if ((person.Name.Length * person.Surname.Length * person.Patronymic.Length * person.Adress.Length * person.Phone.Length) == 0)
                return;
            switch ((person.UniqueId + step + 1) % 5)
            {
                case 0:
                    person.Name = person.Name.Remove((person.UniqueId + step) % person.Name.Length, 1);
                    break;
                case 1:
                    person.Surname = person.Surname.Remove((person.UniqueId + step) % person.Surname.Length, 1);
                    break;
                case 2:
                    person.Patronymic = person.Patronymic.Remove((person.UniqueId + step) % person.Patronymic.Length, 1);
                    break;
                case 3:
                    person.Adress = person.Adress.Remove((person.UniqueId + step) % person.Adress.Length, 1);
                    break;
                case 4:
                    person.Phone = person.Phone.Remove((person.UniqueId + step) % person.Phone.Length, 1);
                    break;
            };
        }
        protected override void AddRandomSymbolError(PersonViewModel person, int step)
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
