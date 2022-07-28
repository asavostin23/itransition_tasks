namespace Task5.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Body { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public Message(string title, string body, string sender, string reciever)
        {
            Title = title;
            Body = body;
            Sender = sender;
            Reciever = reciever;
            CreatedDate = DateTime.Now;
        }
    }
}
