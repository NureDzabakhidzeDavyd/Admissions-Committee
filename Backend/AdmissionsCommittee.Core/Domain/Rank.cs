using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Rank))]
    public class Rank
    {
        [Key]
        public int RankId { get; set; }
        public string RankName { get; set; }
    }
}
