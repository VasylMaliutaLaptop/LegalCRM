namespace LegalCRM.Shared.Case
{
    public abstract class CaseBase
    {
        public int Id { get; protected set; }
        public int ClientId { get; protected set; }
        public CaseStatus Status { get; protected set; } = CaseStatus.Draft;

        // Audit
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public string? CreatedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public string? UpdatedBy { get; protected set; }
    }
}
