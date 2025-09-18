using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LegalCRM.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityUserContext<User>(options)
    {
        public DbSet<Case> Cases => Set<Case>();
        public DbSet<Client> Clients => Set<Client>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Наследование дел (TPH по умолчанию + дискриминатор)
            builder.Entity<Case>()
             .HasDiscriminator<string>("CaseType")
             .HasValue<Case>("Base");// можно оставить дефолтный дискриминатор

            builder.Entity<Case>()
              .HasOne(c => c.Client)
              .WithMany(cl => cl.Cases)
              .HasForeignKey(c => c.ClientId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
