using Microsoft.AspNetCore.Mvc;
using staj3.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace staj3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        IList<Location> locationList = new List<Location>() {
        new() { Id = 1, Name = "John", X = 13.8, Y= 12.5} ,
        new() {  Id = 1, Name = "John", X = 13.1, Y = 9.0  } ,
        new() { Id = 1, Name = "John", X = 13.1, Y = 9.0 } ,
        new() { Id = 1, Name = "John", X = 13.1, Y = 9.0 } ,
        new() { Id = 1, Name = "John", X = 13.1, Y = 9.0  }
          };
        // GET: api/<ValuesController>
        [HttpGet]
        public List<Location> Get()
        {
            var _loc = locationList.ToList<Location>();
            return _loc;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var location = locationList.Where(s => s.Id  == id).ToList<Location>();
            return "başarılı";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post(Location loc)
        {
            locationList.Add(loc);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var teenAgerStudents = locationList.Where(s => s.Id == id).ToList<Location>();
            locationList.Remove(teenAgerStudents);
        }
    }
}
