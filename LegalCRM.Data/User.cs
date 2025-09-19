using Microsoft.AspNetCore.Identity;

namespace LegalCRM.Data
{
    public class User : IdentityUser
    {
        public ICollection<Case> Cases { get; set; } = [];
        public ICollection<Client> Clients { get; set; } = [];
    }
}
