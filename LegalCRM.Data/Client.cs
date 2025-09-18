using LegalCRM.Shared.Client;

namespace LegalCRM.Data
{
    public class Client
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; } = string.Empty;

        public ClientStatus Status { get; set; }
        public ICollection<Case> Cases { get; set; } = [];
        public ContactInfo ContactInfo { get; set; } = null!;
    }
}
