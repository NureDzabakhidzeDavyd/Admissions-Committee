using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Request
{
    public class UpdateFacultyRequest
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
