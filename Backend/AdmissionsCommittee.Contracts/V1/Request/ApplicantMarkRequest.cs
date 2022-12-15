using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Request
{
    public class ApplicantMarkRequest
    {
        public int EieId { get; set; }
        public int MarkValue { get; set; }
    }
}
