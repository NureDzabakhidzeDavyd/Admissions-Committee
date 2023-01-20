using AdmissionsCommittee.Contracts.V1.Request.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Request.Applicant
{
    public class CreateApplicantRequest
    {
        public CreatePersonRequest Person { get; set; }

        public CreateMarkRequest[] Marks { get; set; }
        public float Certificate { get; set; }
    }
}
