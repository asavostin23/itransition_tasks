using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task5.Models;

namespace Task5.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationContext db;
        public MessagesController(ApplicationContext db) => this.db = db;
        [HttpGet("{reciever}")]
        public async Task<IActionResult> Get(string reciever)
        {
            List<Message> messages = await db.Messages.Where(message => message.Reciever == reciever).ToListAsync();
            return new JsonResult(messages);
        }
        [HttpPost]
        public async Task Post([FromBody]MessageModel messageModel)
        {
            db.Messages.Add(new Message(messageModel.Title, messageModel.Body, messageModel.Sender, messageModel.Reciever));
            await db.SaveChangesAsync();
        }
        public class MessageModel
        {
            public string Title { get; set; }
            public string Reciever { get; set; }
            public string Body { get; set; }
            public string Sender { get; set; }
        }
    }
}
