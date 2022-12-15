using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain.Filters
{
    public class SortFilter
    {
        public string? Field { get; set; } = null;

        public bool Descending { get; set; }
    }
}
