using AdmissionsCommittee.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Data
{
    public interface ISpecialtyRepository : IRepository<Speciality>
    {
        public Task<CompetitiveScoreStatistic> CompareApplicantCompetitiveScore(int applicantCompetitiveScore, int specialityId);
        public Task<IEnumerable<Speciality>> GetSpecialitiesInformationsAsync();
    }
}
