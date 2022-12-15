using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Data
{
    public interface IUnitOfWork
    {
        public ISpecialtyRepository SpecialtyRepository { get; set; }
        public IFacultyRepository FacultyRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IRankRepository RankRepository { get; set; }
        public IPersonRepository PersonRepository { get; set; }
        public IWorkingRepository WorkingRepository { get; set; }
        public IApplicantRepository ApplicantRepository { get; set; }
        public IStatementRepository StatementRepository { get; set; }
        public IEieRepository EieRepository { get; set; }
        public ICoefficientRepository CoefficientRepository { get; set; }
        public IStatisticRepository StatisticRepository { get; set; }
        public IMarkRepository MarkRepository { get; set; }
    }
}
