using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Employee))]
    public class Employee
    {
        [ExplicitKey]
        public int EmployeeId { get; set; }
        [Write(false)]
        public Person Person { get; set; }
        public int FacultyId { get; set; }
        [Write(false)]
        public Faculty Faculty { get; set; }
        [Write(false)]
        public List<Working> Working { get; set; } = new List<Working>();
        public string CareerInfo { get; set; }
    }
}
