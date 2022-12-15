using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain.Filters
{
    public class DynamicFilter
    {
        public string FieldName { get; set; }
        public int FieldType { get; set; }
        public string Value { get; set; }
    }
    public class DynamicFilters
    {
        public IEnumerable<DynamicFilter>? Filters { get; set; } = null;
    }
}
