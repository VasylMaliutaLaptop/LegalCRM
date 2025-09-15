namespace LegalCRM.Shared.Client
{
    public class ContactInfo
    {
        public string Name { get; protected set; } = string.Empty;
        public string Surname { get; protected set; } = string.Empty;
        public string Phone { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;
    }
}
