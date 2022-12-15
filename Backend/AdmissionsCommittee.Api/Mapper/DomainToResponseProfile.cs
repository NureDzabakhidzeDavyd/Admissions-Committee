using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Domain;
using AutoMapper;

namespace AdmissionsCommittee.Api.Mapper
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Speciality, SpecialtyResponse>();
            CreateMap<Faculty, FacultyResponse>();
            CreateMap<Employee, EmployeeResponse>();
            CreateMap<Person, PersonResponse>();
            CreateMap<Working, WorkingResponse>();
            CreateMap<Rank, RankResponse>();
            CreateMap<Statement, StatementResponse>();
            CreateMap<Lookup, LookupResponse>();
            CreateMap<Eie, EieResponse>();
            CreateMap<Coefficient, CoefficientResponse>();
            CreateMap<Statistic, StatisticResponse>();
            CreateMap<Applicant, ApplicantResponse>();
            CreateMap<Mark, MarkResponse>();
            CreateMap<CompetitiveScoreStatistic, CompetitiveScoreStatisticResponse>();
        }
    }
}
