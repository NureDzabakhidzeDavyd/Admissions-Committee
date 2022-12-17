using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Request
{
    public class UpdateWorkingRequest
    {
        public int RankId { get; set; }
        public int IssuedYear { get; set; }
    }
}
