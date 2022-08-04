using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using staj4.Data;
using staj4.Models;

namespace staj4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        private readonly LocationData dbContext;


        public LocationController(LocationData dbContext)
        {
            this.dbContext = dbContext;         
        }

        // GET: api/<LocationController>
        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            
            return Ok(await dbContext.Locations.ToListAsync());
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocations(int id)
        {
            var locations = await dbContext.Locations.FindAsync(id);
            if (locations == null) return NotFound();
            return Ok(locations);
        }

        // POST api/<LocationController>
        [HttpPost]
        public async Task<IActionResult> AddLocation(AddLocationRequest addContacsRequest)
        {
            Location temp = new Location();
            //temp =dbContext.Locations.FromSqlRaw("SELECT * FROM Locations name = " + addContacsRequest.Name);
            
            temp = dbContext.Locations.FirstOrDefault(x => x.Name.ToLower() == addContacsRequest.Name.ToLower());

            
            if (temp == null)
            {

                if (addContacsRequest.Name != "string" && addContacsRequest.X != 0 && addContacsRequest.Y != 0)
                {
                    var location = new Location()
                    {
                        
                        Name = addContacsRequest.Name,
                        X = addContacsRequest.X,
                        Y = addContacsRequest.Y

                    };
                    await dbContext.Locations.AddAsync(location);
                    await dbContext.SaveChangesAsync();
                    return Ok(location);
                }

                else
                {
                    return BadRequest("Boş veri bilgisi girmeyiniz.");
                }
            }
            else
            {
                return BadRequest("Aynı isimde değer bulunmaktadır. Farklı veri giriniz.");
            }
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocations(int id, UpdateLocationRequest updateContactRequest)
        {
            var loc = await dbContext.Locations.FindAsync(id);

            if (loc != null)
            {
                if (updateContactRequest.Name != "string")
                    loc.Name = updateContactRequest.Name;
                if(updateContactRequest.X != 0)
                    loc.X = updateContactRequest.X;
                if(updateContactRequest.Y != 0)
                    loc.Y = updateContactRequest.Y;

                await dbContext.SaveChangesAsync();

                return Ok("Başarıyla güncellendi");
            }


            return BadRequest("Bulunamadı");
        }

        // DELETE api/<LocationController>/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var contact = await dbContext.Locations.FindAsync(id);
            if (contact != null)
            {
                dbContext.Locations.Remove(contact);
                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }
            return BadRequest("Bulunamadı");
        }
    }
}
