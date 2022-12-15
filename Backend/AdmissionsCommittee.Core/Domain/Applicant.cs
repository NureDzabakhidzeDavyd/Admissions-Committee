using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Applicant))]
    public class Applicant
    {
        [ExplicitKey]
        public int ApplicantId { get; set; }
        public float Certificate { get; set; }

        //[Write(false)]
        //public IEnumerable<Statement> Statements { get; set; }
        [Write(false)]
        public Person Person { get; set; }
    }
}
