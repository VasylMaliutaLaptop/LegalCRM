using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LegalCRM.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityUserContext<User>(options)
    {
        public DbSet<Case> Cases => Set<Case>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<ContactInfo> ContactInfos => Set<ContactInfo>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Case>()
              .HasOne(c => c.Client)
              .WithMany(cl => cl.Cases)
              .HasForeignKey(c => c.ClientId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Client>()
                .HasOne(c => c.ContactInfo)
                .WithOne(ci => ci.Client)
                .HasForeignKey<ContactInfo>(ci => ci.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
