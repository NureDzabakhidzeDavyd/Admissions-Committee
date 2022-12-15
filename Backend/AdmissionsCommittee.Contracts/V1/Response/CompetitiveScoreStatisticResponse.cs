using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class CompetitiveScoreStatisticResponse
    {
        public int TotalApplicantsCount { get; set; }
        public int ApplicantCompetitiveScorePosition { get; set; }
        public double AverageCompetitiveScore { get; set; }
    }
}
