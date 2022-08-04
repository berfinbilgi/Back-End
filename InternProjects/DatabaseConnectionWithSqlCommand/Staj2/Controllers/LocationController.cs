using Microsoft.AspNetCore.Mvc;
using Staj2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Staj2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        public static List<Location> _location = new List<Location>();

        // GET: api/<LocationController>
        [HttpGet]
        public List<Location> Get()
        {
            return _location;
        }

        // GET api/<LocationController>/5
       [HttpGet("{id}")]
        public async  Task<ActionResult<Location>> Get(int id)
        {
            var location = _location.Find(h => h.Id == id);

            if (location == null)
                return BadRequest("Not Found");
            return Ok(location);
        }
      
        // POST api/<LocationController>
        [HttpPost]
        public string Post(Location _Location){

            _Location.Id = new Random().Next();
            _location.Add(_Location);
            return "eklendi";
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Location>> Update(int id, Location  request)
        {
            var loc = _location.Find(h => h.Id == id);

            if (loc == null)
                return BadRequest("Not Found");


            loc.Name = request.Name;
            loc.X = request.X;
            loc.Y = request.Y;

            return Ok(loc);
          
        }

        // DELETE api/<LocationController>/5 
         [HttpDelete("{id}")]
         
         public async Task<ActionResult<Location>> Delete( int id)
         {
            var loc = _location.Find(h => h.Id == id);

            if (loc == null)
                return BadRequest("Not found");

           _location.Remove(loc);
            return Ok(loc); 
         }

        
    }
}
