namespace LegalCRM.Shared.Client
{
    public class ClientCreateDto
    {
        public ContactInfoDto ContactInfo { get; set; } = new ContactInfoDto();
        public ClientStatus Status { get; set; } = ClientStatus.Draft;
    }
}
