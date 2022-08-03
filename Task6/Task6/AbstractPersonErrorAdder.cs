using System.Text;
using Task6.Models;

namespace Task6
{
    public abstract class AbstractPersonErrorAdder
    {
        protected float errorLevel;
        protected float ErrorLevel
        {
            get
            {
                return errorLevel;
            }
            set
            {
                if (value > 0 && value <= 1000) errorLevel = (float)Math.Round(value, 1);
                else
                    errorLevel = 0;
            }
        }
        public AbstractPersonErrorAdder(float errorLevel)
        {
            ErrorLevel = errorLevel;
        }
        public abstract PersonViewModel AddErrors(PersonViewModel person);
        protected virtual void AddSwapSymbolError(PersonViewModel person, int step)
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
        protected virtual void AddSymbolRemovingError(PersonViewModel person, int step)
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
    }

}
