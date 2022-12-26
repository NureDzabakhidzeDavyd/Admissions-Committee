using AdmissionsCommittee.Contracts.V1.Request.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Request
{
    public class UpdateApplicantRequest
    {
        public float Certificate { get; set; }    
        public UpdatePersonRequest Person { get; set; }
    }
}
