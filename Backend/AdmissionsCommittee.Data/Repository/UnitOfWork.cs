using AdmissionsCommittee.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ISpecialtyRepository specialtyRepository, 
            IFacultyRepository facultyRepository,
            IEmployeeRepository employeeRepository,
            IRankRepository rankRepository,
            IPersonRepository personRepository,
            IWorkingRepository workingRepository,
            IApplicantRepository applicantRepository,
            IStatementRepository statementRepository,
            IEieRepository eieRepository,
            ICoefficientRepository coefficientRepository,
            IStatisticRepository statisticRepository,
            IMarkRepository markRepository)
        {
            SpecialtyRepository = specialtyRepository;
            FacultyRepository = facultyRepository;
            EmployeeRepository = employeeRepository;
            RankRepository = rankRepository;
            PersonRepository = personRepository;
            WorkingRepository = workingRepository;
            StatementRepository = statementRepository;
            ApplicantRepository = applicantRepository;
            EieRepository = eieRepository;
            CoefficientRepository = coefficientRepository;
            StatisticRepository = statisticRepository;
            MarkRepository = markRepository;
        }

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
