using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task5.Models;

namespace Task5.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutocompleteController : ControllerBase
    {
        private readonly ApplicationContext db;
        public AutocompleteController(ApplicationContext db) => this.db = db;
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<string> users = await db.Messages.Select(message => message.Sender)
                .Union(db.Messages.Select(message => message.Reciever))
                .ToListAsync();
            return new JsonResult(users);
        }
    }
}
