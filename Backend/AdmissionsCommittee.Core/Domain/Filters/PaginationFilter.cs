using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain.Filters
{
    public class PaginationFilter
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; }
    }
}
