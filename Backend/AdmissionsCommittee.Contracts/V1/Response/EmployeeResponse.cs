using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public PersonResponse Person { get; set; }
        public IEnumerable<WorkingResponse> Working { get; set; }
        public FacultyResponse Faculty { get; set; }
        public string CareerInfo { get; set; }
    }
}
