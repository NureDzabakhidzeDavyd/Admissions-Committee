using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain.Filters
{
    public class PaginationFilter
    {
        public int Page { get; set; } = 1;
        
        [Range(1, 50)]
        public int Size { get; set; } = 10;
    }
}
