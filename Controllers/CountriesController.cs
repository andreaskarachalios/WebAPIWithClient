using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers{

    [Route("api/[controller]")]
    public class CountriesController:ControllerBase
    {
        private WorldDbContext dbContext;
        
        public CountriesController() {
            var connString = "server=localhost;port=3306;database=world;userid=test;pwd=test;sslmode=Required";
            this.dbContext = WorldDbContextFactory.Create(connString);
        }

        [HttpGet]
        public ActionResult Get() {
            return Ok(this.dbContext.Country.ToArray());
        }
    }
}
