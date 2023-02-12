using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Domain;
using AutoMapper;
using HandbookActivity.Contracts.Responses;
using HandbookActivity.Core.Domain;

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
            CreateMap<Faculty, FacultyResponse>();
            CreateMap<UserProfile, UserProfileResponse>();
            
            CreateMap(typeof(Page<>), typeof(ApiListResponse<>)).ConvertUsing(typeof(PageListConverter<,>));
        }
    }
    
    class PageListConverter<T, TR> : ITypeConverter<Page<T>, ApiListResponse<TR>>
    {
        public ApiListResponse<TR> Convert(Page<T> source, ApiListResponse<TR> destination, ResolutionContext context)
        {
            var data = context.Mapper.Map<IEnumerable<TR>>(source.Data);
            return ApiListResponse<TR>.CreateListResponse(data, source.TotalCount);
        }
    }
    
}
