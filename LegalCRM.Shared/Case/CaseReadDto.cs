using LegalCRM.Shared.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalCRM.Shared.Case
{
    public class CaseReadDto
    {
        public int Id { get; set; }
        public ClientDTO Client { get; set; } = null!;
        public CaseStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
