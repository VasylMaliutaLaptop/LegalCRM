using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalCRM.Shared.Contracts
{
    public class AuthResponseDTO
    {
        public string Access_token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
