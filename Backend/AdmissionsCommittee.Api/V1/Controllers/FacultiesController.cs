using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain.Filters;
using AutoMapper;
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

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] SortFilter? sortFilter = null,
            [FromQuery] DynamicFilters? dynamicFilters = null)
        {
            var employees = await _unitOfWork.FacultyRepository.PaginateAsync(paginationFilter, sortFilter, dynamicFilters);
            var response = _mapper.Map<IEnumerable<FacultyResponse>>(employees);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByid([FromRoute] int id)
        {
            var faculty = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (faculty is null)
            {
                return NotFound();
            }
            var response = _mapper.Map<FacultyResponse>(faculty);
            return Ok(response);
        }
    }
}
