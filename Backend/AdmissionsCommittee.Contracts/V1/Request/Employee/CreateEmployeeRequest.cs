using AdmissionsCommittee.Contracts.V1.Request.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Request.Employee
{
    public class CreateEmployeeRequest
    {        
        public CreatePersonRequest Person { get; set; }
        public int FacultyId { get; set; }
        
        public IEnumerable<CreateWorkingRequest> Working { get; set; }
        public string CareerInfo { get; set; }
    }
}
