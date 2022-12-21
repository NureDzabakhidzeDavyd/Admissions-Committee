﻿using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Services
{
    public interface IGoogleService
    {
        public Task<GoogleTokenBody> RefreshAccessToken(string refreshToken);

        public Task<GoogleTokenBody> GetAccessTokenAsync(string code);

        public Task<UserProfile> GetUserProfile(string accessToken);

        public string WriteJwtToken();
    }
}