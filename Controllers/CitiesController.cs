using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers{

    [Route("api/[controller]")]
    public class CitiesController:ControllerBase
    {
        private WorldDbContext dbContext;
        
        public CitiesController() {
            var connString = "server=localhost;port=3306;database=world;userid=test;pwd=test;sslmode=Required";
            this.dbContext = WorldDbContextFactory.Create(connString);
        }

        [HttpGet]
        public ActionResult Get() {
            return Ok(this.dbContext.City.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            var city = this.dbContext.City.FirstOrDefault(c=>c.ID == id);
            if(city != null)
                return Ok(city);
            return NotFound();
        }

        [HttpGet("cc/{cc}")]
        public ActionResult Get(string cc) {
            var cities = this.dbContext.City.Where(c=>c.CountryCode == cc).ToArray();
            if(cities.Any())
                return Ok(cities);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Post([FromBody]City city){
            if (!this.ModelState.IsValid) {
                return BadRequest();
            }
            this.dbContext.City.Add(city);
            this.dbContext.SaveChanges();
            return Created("api/cities/{city.ID}", city);           
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]City city){
            if (!this.ModelState.IsValid)
                return BadRequest();

            var oldCity = this.dbContext.City.SingleOrDefault(c=>c.ID == city.ID);
            if(oldCity != null){
                this.dbContext.Entry(oldCity).CurrentValues.SetValues(city);
                this.dbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            var oldCity = this.dbContext.City.SingleOrDefault(c=>c.ID == id);
            if(oldCity != null){
                this.dbContext.City.Remove(oldCity);
                this.dbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

    }
}
