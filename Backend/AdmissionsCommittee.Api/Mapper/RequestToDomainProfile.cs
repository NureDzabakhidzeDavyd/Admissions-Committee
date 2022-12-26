using AdmissionsCommittee.Contracts.V1.Request;
using AdmissionsCommittee.Contracts.V1.Request.Employee;
using AdmissionsCommittee.Contracts.V1.Request.Person;
using AdmissionsCommittee.Core.Domain;
using AutoMapper;

namespace AdmissionsCommittee.Api.Mapper
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<UpdateEmployeeRequest, Employee>();
            CreateMap<CreateEmployeeRequest, Employee>();

            CreateMap<UpdatePersonRequest, Person>();
            CreateMap<CreatePersonRequest, Person>();

            CreateMap<UpdateApplicantRequest, Applicant>();

            CreateMap<UpdateWorkingRequest, Working>();
            CreateMap<CreateWorkingRequest, Working>();
        }
    }
}
