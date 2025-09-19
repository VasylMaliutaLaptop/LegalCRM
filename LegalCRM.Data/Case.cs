using LegalCRM.Shared.Case;

namespace LegalCRM.Data
{
    public class Case
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public CaseStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; } = string.Empty;   
    }
}
