using Microsoft.EntityFrameworkCore;
using staj5_2.Models;

namespace staj5_2.Data
{
    public class LocationData: DbContext
    {
        public LocationData(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Location> locations { get; set; }
    }
}
