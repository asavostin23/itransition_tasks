namespace Task4.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string Password { get; set; }
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            DateTime now = DateTime.Now;
            CreatedDate = now;
            LastLoginDate = now;
        }
    }
}
