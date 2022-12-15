using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class ApplicantResponse
    {
        public int ApplicantId { get; set; }
        public PersonResponse Person { get; set; }
        public float Certificate { get; set; }
    }
}
