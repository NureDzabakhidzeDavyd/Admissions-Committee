using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class GoogleAuthCodeResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
