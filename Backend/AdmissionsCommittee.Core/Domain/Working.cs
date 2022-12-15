using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Working))]
    public class Working
    {
        [Key]
        public int WorkingId { get; set; }
        public int EmployeeId { get; set; }
        public int RankId { get; set; }
        [Write(false)]
        public Rank Rank { get; set; }
        public int IssuedYear { get; set; }       
    }
}
