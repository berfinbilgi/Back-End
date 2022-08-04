using Microsoft.EntityFrameworkCore;
using staj4.Models;

namespace staj4.Data
{
    public class LocationData: DbContext 
    {
        public LocationData(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

    }
}
