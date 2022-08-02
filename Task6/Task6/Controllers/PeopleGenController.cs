﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task6.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleGenController : ControllerBase
    {
        private readonly task6dbContext db;
        private AbstractUserGenerator userGenerator;
        public PeopleGenController(task6dbContext db)
        {
            this.db = db;
        }
        [HttpGet("{region}")]
        public IActionResult Get(string region, int seed, int page)
        {
            switch (region)
            {
                case "by":
                    userGenerator = new UserGeneratorBy(db, seed);
                    break;
            }
            return new JsonResult(userGenerator.GetPeople(page));
        }
    }
}