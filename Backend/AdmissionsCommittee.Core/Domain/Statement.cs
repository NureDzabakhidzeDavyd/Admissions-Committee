using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Statement))]
    public class Statement
    {
        [Key]
        public int StatementId { get; set; }
        public int ApplicantId { get; set; }
        public int SpecialityId { get; set; }

        [Write(false)]
        public Lookup Applicant { get; set; } = new Lookup();
        [Write(false)]
        public Lookup Speciality { get; set; } = new Lookup();
    }
}
