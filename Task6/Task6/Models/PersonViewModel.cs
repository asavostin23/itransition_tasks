namespace Task6.Models
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public int UniqueId { get; set; }
        public PersonViewModel(string surname, string name, string adress, int id)
        {
            Name = name;
            Surname = surname;
            Adress = adress;
            UniqueId = id;
        }
    }
}
