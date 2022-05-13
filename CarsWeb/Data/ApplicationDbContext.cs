using CarsWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsWeb.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CarCategory> CarCategories { get; set; }
    }
}
