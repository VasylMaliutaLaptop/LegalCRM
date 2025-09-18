using LegalCRM.Shared.Client;

namespace LegalCRM.Data
{
    public class Client
    {
        public int Id { get; set; }
        public ClientStatus Status { get; set; }
        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public ICollection<Case> Cases { get; set; } = [];
    }
}
