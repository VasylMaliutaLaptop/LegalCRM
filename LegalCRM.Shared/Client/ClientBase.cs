namespace LegalCRM.Shared.Client
{
    public abstract class ClientBase
    {
        public int Id { get; protected set; }
        public ContactInfo Contact { get; protected set; } = new ContactInfo();
        public ClientStatus Status { get; protected set; } = ClientStatus.Draft;
        public List<int> CaseIds { get; protected set; } = new List<int>();

        // Audit
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public string? CreatedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public string? UpdatedBy { get; protected set; }
    }
}
