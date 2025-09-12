using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LegalCRM.Data
{
public class AppDbContext : IdentityUserContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

}
