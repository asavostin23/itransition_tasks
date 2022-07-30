using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Task5.Models;

namespace Task5.Hubs
{
    public class MessageHub : Hub
    {
        protected ApplicationContext db;
        public static Dictionary<string, string> NamesConnectionIds { get; set; } = new();
        public MessageHub(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        public async Task SendMessages()
        {
            string userName;
            lock (NamesConnectionIds)
            {
                userName = NamesConnectionIds[Context.ConnectionId];
            }
            List<Message> messages = db.Messages.Where(message => message.Reciever == userName).ToList();
            await Clients.Caller.SendAsync("ReceiveMessages", messages);
        }
        public void Register(string name)
        {
            lock (NamesConnectionIds)
            {
                MessageHub.NamesConnectionIds[Context.ConnectionId] = name;
            }
        }
        public async Task SendAutocompleteData()
        {
            List<String> users = db.Messages.Select(message => message.Sender)
                .Union(db.Messages.Select(message => message.Reciever))
                .ToList();
            await Clients.Caller.SendAsync("RecieveAutocompleteData", users);
        }
        public async Task GetMessage(string sender, string title, string body, string reciever)
        {
            db.Messages.Add(new Message(title, body, sender, reciever));
            await db.SaveChangesAsync();
            string recieverConnectionId = MessageHub.NamesConnectionIds.FirstOrDefault(el => el.Value == reciever).Key;
            if (recieverConnectionId != null)
                await Clients.Client(recieverConnectionId).SendAsync("UpdateMessages");
        }
    }
}
