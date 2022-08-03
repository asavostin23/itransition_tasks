using Microsoft.EntityFrameworkCore;
using Task6.Models;

namespace Task6
{
    public abstract class AbstractUserGenerator
    {
        protected virtual int Seed { get; set; }
        public abstract ICollection<PersonViewModel> GetPeople(int page);

        protected AbstractUserGenerator(task6dbContext db, int seed, float errorLevel)
        {
            Seed = seed;
            this.db = db;
            ErrorLevel = errorLevel;
        }
        protected task6dbContext db;
        private float errorLevel;
        protected virtual float ErrorLevel
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
    }
}
