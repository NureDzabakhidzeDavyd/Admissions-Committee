using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Request
{
    public class UpdateEmployeeRequest
    {
        public UpdatePersonRequest Person { get; set; }
        //public IEnumerable<UpdateWorkingRequest> Working { get; set; }
        public int FacultyId { get; set; }
        public string CareerInfo { get; set; }
    }
}
