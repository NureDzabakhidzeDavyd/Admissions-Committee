using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class WorkingResponse
    {
        
        public int WorkingId { get; set; }
        public RankResponse Rank { get; set; }
        public int IssuedYear { get; set; }
    }
}
