using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Mark))]
    public class Mark
    {
        [Key]
        public int MarkId { get; set; }
        public int ApplicantId { get; set; }
        public int EieId { get; set; }
        [Write(false)]
        public Eie Eie { get; set; }
        public int MarkValue { get; set; }
        public int WriteYear { get; set; }
    }
}
