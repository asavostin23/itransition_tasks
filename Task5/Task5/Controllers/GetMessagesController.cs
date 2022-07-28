using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task5.Models;

namespace Task5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetMessagesController : ControllerBase
    {
        public ApplicationContext db;
        public GetMessagesController(ApplicationContext db) => this.db = db;
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return new JsonResult(name);
        }
    }
}
