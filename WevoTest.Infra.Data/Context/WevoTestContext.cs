using Microsoft.EntityFrameworkCore;
using WevoTest.Domain.Entities;
using WevoTest.Infra.Data.Mappings;

namespace WevoTest.Infra.Data.Context
{
    public class WevoTestContext : DbContext
    {
        public WevoTestContext(DbContextOptions<WevoTestContext> options) : base(options)
        {
            //creates database and applies migration & seed
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());            
            SeedInitialData(modelBuilder);            
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() 
                {
                    Id = 1, 
                    Name = "Rafael Baptista", 
                    Email = "rafabap100@gmail.com", 
                    Cellphone = "11995846153", 
                    CPF = "11111111111", 
                    Gender = "Masculino" 
                }
                );
        }
    }
}
