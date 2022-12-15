using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Statistic))]
    public class Statistic
    {
        [Key]
        public int StatisticId { get; set; }
        public int SpecialityId { get; set; }
        public int StatisticYear { get; set; }
        public float BudgetMin { get; set; }
        public float BudgetAver { get; set; }
        public float ContractMin { get; set; }
        public float ContractAver { get; set; }
    }
}
