using Entity_Trial;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace Entity_Trial
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Read the "AppDb" conn string from App.config
            var cs = ConfigurationManager
                       .ConnectionStrings["AppDb"]!
                       .ConnectionString!;
            options.UseSqlServer(cs);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Person>()
                 .Property(p => p.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
