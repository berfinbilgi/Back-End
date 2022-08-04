using Microsoft.AspNetCore.Mvc;
using staj4.Data;
using staj4.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace staj4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationControllerLinq : ControllerBase
    {
        private readonly LocationData dbContext;


        public LocationControllerLinq(LocationData dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<LocationControllerLinq>
        [HttpGet]
        public IQueryable<Location> Get()
        {
            Location location = new Location();

            var loc = from dbContext in dbContext.Locations.OrderBy(x => x.Id) select dbContext;

            return loc;

        }

        // GET api/<LocationControllerLinq>/5
        [HttpGet("{id}")]
        public Location Get(int id)
        {
            var loc = dbContext.Locations.Single(x => x.Id == id);

            return loc;
        }

        // POST api/<LocationControllerLinq>
        [HttpPost]
        public string Post(AddLocationRequest location)
        {
            var loc = (from dbContext in dbContext.Locations where (dbContext.Name.ToLower() == location.Name.ToLower()) select dbContext).FirstOrDefault();
            if (loc != null)
            {
                return "aynı değeri girmeyiniz";
            }
            else
            {
                if (location.Name != "string" && location.X != 0 && location.Y != 0)
                {
                    var _loc = new Location()
                    {

                        Name = location.Name,
                        X = location.X,
                        Y = location.Y
                    };
                    dbContext.Locations.Add(_loc);

                }
                else
                {
                    return "boş veri girmeyiniz.";
                }
                dbContext.SaveChanges();
            }
            return "Başarılı";
        }

        // PUT api/<LocationControllerLinq>/5
        [HttpPut]
        public string Put(UpdateLocationRequest location, int id)
        {
            var loc = dbContext.Locations.Single(x => x.Id == id);

            //var update = from dbcontext in dbContext.Locations where dbcontext.Id == location.Id select dbcontext;
            if (loc != null)
            {
                if (location.Name != "string")
                    loc.Name = location.Name;

                if (location.X != 0)
                    loc.X = location.X;

                if (location.Y != 0)
                    loc.Y = location.Y;
                dbContext.SaveChanges();
                return "Başarılı";
            }
            else
                return "geçerli id giriniz..";

        }

        // DELETE api/<LocationControllerLinq>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleteloc = from dbcontext in dbContext.Locations where dbcontext.Id == id select dbcontext;
            if (deleteloc != null)
                dbContext.RemoveRange(deleteloc);
            dbContext.SaveChanges();

        }
    }
}
