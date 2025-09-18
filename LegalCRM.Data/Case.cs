using LegalCRM.Shared.Case;

namespace LegalCRM.Data
{
    public class Case
    {
        public int Id { get; set; }
        public int ClientId { get; set; }       // FK (NOT NULL)
        public Client Client { get; set; } = null!;
        public CaseStatus Status { get; set; }  // ваш enum
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
