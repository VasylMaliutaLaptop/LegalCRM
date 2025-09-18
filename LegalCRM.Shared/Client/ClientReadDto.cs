using LegalCRM.Shared.Case;

namespace LegalCRM.Shared.Client
{
    public class ClientReadDto
    {
        public int Id { get; set; }
        public ClientStatus Status { get; set; } = ClientStatus.Draft;
        public List<CaseReadDto> Cases { get; set; } = [];

        public ContactInfoDto ContactInfo { get; set; } = null!;
        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
