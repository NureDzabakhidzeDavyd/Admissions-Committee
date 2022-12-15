using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class CoefficientResponse
    {
        public int CoefficientId { get; set; }
        public EieResponse Eie { get; set; }

        public float CoefficientValue { get; set; }
    }
}
