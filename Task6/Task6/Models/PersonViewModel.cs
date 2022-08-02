namespace Task6.Models
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Adress { get; set; }
        public int UniqueId { get; set; }
        public PersonViewModel(string surname, string name, string patronymic, string adress, int id)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Adress = adress;
            UniqueId = id;
        }
    }
}
