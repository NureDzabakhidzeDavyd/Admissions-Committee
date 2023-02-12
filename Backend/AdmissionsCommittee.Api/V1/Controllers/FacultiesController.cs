using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain.Filters;
using AutoMapper;
using HandbookActivity.Contracts.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionsCommittee.Api.V1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacultiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FacultiesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] SortFilter? sortFilter = null,
            [FromQuery] DynamicFilters? dynamicFilters = null)
        {
            var employees = await _unitOfWork.FacultyRepository.PaginateAsync(paginationFilter, sortFilter, dynamicFilters);
            var response = _mapper.Map<ApiListResponse<FacultyResponse>>(employees);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var faculty = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (faculty is null)
            {
                return NotFound($"Faculty with {id} doesn't exist".ToErrorResponse());
            }
            var response = _mapper.Map<FacultyResponse>(faculty);
            return Ok(response.ToApiResponse());
        }
    }
}
