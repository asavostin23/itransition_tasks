using Microsoft.EntityFrameworkCore;
using Task6.Models;

namespace Task6
{
    public abstract class AbstractUserGenerator
    {
        protected virtual int Seed { get; set; }
        public abstract ICollection<PersonViewModel> GetPeople(int page);
        public abstract ICollection<PersonViewModel> GetFirstPage();

        protected AbstractUserGenerator(int seed)
        {
            Seed = seed;
        }

    }
}
