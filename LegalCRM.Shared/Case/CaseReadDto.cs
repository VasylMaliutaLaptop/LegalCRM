namespace LegalCRM.Shared.Case
{
    public class CaseReadDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public CaseStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
