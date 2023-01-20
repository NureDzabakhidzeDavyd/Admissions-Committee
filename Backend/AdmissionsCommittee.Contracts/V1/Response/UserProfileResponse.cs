using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class UserProfileResponse
    {
        public int UserId { get; set; }

        public string UserGoogleId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Avatar { get; set; }

        public string Country { get; set; }

        public string RefreshToken { get; set; }
    }
}
