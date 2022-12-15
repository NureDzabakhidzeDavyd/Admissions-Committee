using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class MarkResponse
    {
        public int MarkId { get; set; }
        public int ApplicantId { get; set; }
        public EieResponse Eie { get; set; }
        public int MarkValue { get; set; }
        public int WriteYear { get; set; }
    }
}
