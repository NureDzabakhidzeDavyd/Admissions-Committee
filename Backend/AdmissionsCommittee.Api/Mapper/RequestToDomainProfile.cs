using AdmissionsCommittee.Contracts.V1.Request;
using AdmissionsCommittee.Core.Domain;
using AutoMapper;

namespace AdmissionsCommittee.Api.Mapper
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<UpdateEmployeeRequest, Employee>();
            CreateMap<UpdatePersonRequest, Person>();
        }
    }
}
