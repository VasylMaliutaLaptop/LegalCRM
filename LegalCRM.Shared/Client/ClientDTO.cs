using LegalCRM.Shared.Case;

namespace LegalCRM.Shared.Client
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public ClientStatus Status { get; set; } = ClientStatus.Draft;

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
