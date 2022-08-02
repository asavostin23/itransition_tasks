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
        protected virtual PersonViewModel Person { get; set; }
        public AbstractPersonErrorAdder(PersonViewModel person, float errorLevel)
        {
            Person = person;
            ErrorLevel = errorLevel;
        }
        public abstract PersonViewModel AddErrors();
    }
}
