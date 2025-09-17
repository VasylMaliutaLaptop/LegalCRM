using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalCRM.Shared.Case
{
    public class CaseCreateDto
    {
        public int ClientId { get; set; }
        public CaseStatus Status { get; set; } = CaseStatus.Draft;
    }
}
