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
        protected char[] symbols;
        public AbstractPersonErrorAdder(float errorLevel)
        {
            ErrorLevel = errorLevel;
        }
        protected virtual void AddSwapSymbolError(PersonViewModel person, int step)
        {
            if ((person.Name.Length * person.Surname.Length * person.Adress.Length * person.Phone.Length) == 0)
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
                    data = person.Adress;
                    break;
                case 3:
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
                    person.Adress = tempData.ToString();
                    break;
                case 3:
                    person.Phone = tempData.ToString();
                    break;
            }
        }
        protected virtual void AddSymbolRemovingError(PersonViewModel person, int step)
        {
            if ((person.Name.Length * person.Surname.Length * person.Adress.Length * person.Phone.Length) == 0)
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
                    person.Adress = person.Adress.Remove((person.UniqueId + step) % person.Adress.Length, 1);
                    break;
                case 3:
                    person.Phone = person.Phone.Remove((person.UniqueId + step) % person.Phone.Length, 1);
                    break;
            };
        }
        public virtual PersonViewModel AddErrors(PersonViewModel person)
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
        protected virtual void AddRandomSymbolError(PersonViewModel person, int step)
        {
            if ((person.Name.Length * person.Surname.Length  * person.Adress.Length * person.Phone.Length) == 0)
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
                    person.Adress = person.Adress.Insert((person.UniqueId + step) % person.Adress.Length, symbols[(person.UniqueId + step) % 43].ToString());
                    break;
                case 3:
                    person.Phone = person.Phone.Insert((person.UniqueId + step) % person.Phone.Length, symbols[(person.UniqueId + step) % 43].ToString());
                    break;
            };
        }
    }

}
