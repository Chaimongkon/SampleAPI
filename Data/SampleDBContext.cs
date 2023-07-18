using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Data
{
    public class SampleDBContext:DbContext
    {
        public SampleDBContext(DbContextOptions<SampleDBContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
