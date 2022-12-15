using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class StatementResponse
    {
        public int StatementId { get; set; }
        public LookupResponse Applicant { get; set; }

        public LookupResponse Speciality { get; set; }

    }
}
