using Microsoft.EntityFrameworkCore;

namespace staj3
{
    public class DbContext
    {

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("server=localhost;port=3306;user=root;password=;database=database");
            }
        }
    }
}
