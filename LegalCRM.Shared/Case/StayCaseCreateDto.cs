using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalCRM.Shared.Case
{
    public class StayCaseCreateDto : CaseCreateDto
    {
        public string? Reason { get; set; }
    }
}
