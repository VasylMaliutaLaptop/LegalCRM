namespace LegalCRM.Shared.Contracts
{
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
