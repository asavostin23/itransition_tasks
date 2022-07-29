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
        public async Task Register(string name)
        {
            lock (NamesConnectionIds)
            {
                MessageHub.NamesConnectionIds[Context.ConnectionId] = name;
            }
        }
    }
}
