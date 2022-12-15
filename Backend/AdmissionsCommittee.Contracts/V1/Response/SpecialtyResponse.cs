using AdmissionsCommittee.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Contracts.V1.Response
{
    public class SpecialtyResponse
    {
        public int SpecialityId { get; set; }
        public int SpecialityCode { get; set; }
        public FacultyResponse Faculty { get; set; }
        public string SpecialityName { get; set; }
        public string EducationDegree { get; set; }
        public string BranchName { get; set; }
        public string OfferType { get; set; }
        public string EducationForm { get; set; }
        public int EducationCost { get; set; }
        public int SeatTotal { get; set; }
        public int SubmittedApplicationsTotal { get; set; }
        public int BudgetTotal { get; set; }
        public int ContractTotal { get; set; }
        public int Quota1Total { get; set; }
        public int Quota2Total { get; set; }

        public IEnumerable<Coefficient> Coefficients { get; set; }
    }
}
