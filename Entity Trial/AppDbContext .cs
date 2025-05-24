using Entity_Trial;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace Entity_Trial
{
    public class AppDbContext : DbContext
    {
        public DbSet<Gage> Gages { get; set; } = null!;
        public DbSet<GageAttachment> GageAttachments { get; set; } = null!;
        public DbSet<CalibrationHistory> CalibrationHistories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var cs = ConfigurationManager
                       .ConnectionStrings["AppDb"]!
                       .ConnectionString!;
            options.UseSqlServer(cs);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Gage>(eb =>
            {
                eb.HasKey(g => g.GageId);
                eb.Property(g => g.InternalIdentNo).HasMaxLength(50);
                eb.Property(g => g.SerialNo).HasMaxLength(50);
                eb.Property(g => g.Type).HasMaxLength(100);
                eb.Property(g => g.ModelNo).HasMaxLength(50);
                eb.Property(g => g.Manufacturer).HasMaxLength(100);
                eb.Property(g => g.UnitOfMeasure).HasMaxLength(50);
                eb.Property(g => g.RangeOrSize).HasMaxLength(50);
                eb.Property(g => g.Accuracy).HasMaxLength(50);
                eb.Property(g => g.ReferenceItem).HasMaxLength(100);
            });

            model.Entity<GageAttachment>()
                 .HasOne(a => a.Gage)
                 .WithMany(g => g.Attachments)
                 .HasForeignKey(a => a.GageId);

            model.Entity<CalibrationHistory>()
                 .HasOne(h => h.Gage)
                 .WithMany(g => g.History)
                 .HasForeignKey(h => h.GageId);
        }
    }
}
