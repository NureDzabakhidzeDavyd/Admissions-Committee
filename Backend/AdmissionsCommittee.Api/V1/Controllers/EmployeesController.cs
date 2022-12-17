using AdmissionsCommittee.Contracts.V1.Request;
using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Domain.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionsCommittee.Api.V1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IMapper mapper, IUnitOfWork unitOfWork)
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
            var employees = await _unitOfWork.EmployeeRepository.PaginateAsync(paginationFilter, sortFilter, dynamicFilters);
            var response = _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid([FromRoute] int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if(employee is null)
            {
                return NotFound();
            }
            var response = _mapper.Map<EmployeeResponse>(employee);
            return Ok(response);
        }

        [HttpPut("{employeeId:int}")]
        public async Task<IActionResult> Update([FromRoute] int employeeId, [FromBody] UpdateEmployeeRequest request)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeId);
            if(employee is null)
            {
                return NotFound();
            }
            employee = _mapper.Map(request, employee);

            var person = await _unitOfWork.PersonRepository.GetByIdAsync(employeeId);
            if(person is null)
            {
                return NotFound();
            }
            person = _mapper.Map(request.Person, person);

            var workings = await _unitOfWork.WorkingRepository.GetEmployeeWorkingsAsync(employeeId);
            if(workings is null)
            {
                return NotFound();
            }


            var newPerson = await _unitOfWork.PersonRepository.UpdateAsync(person);
            var newEmployee = await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
            newEmployee.Person = newPerson;

            var response = _mapper.Map<EmployeeResponse>(newEmployee);

            return Ok(newEmployee);
        }
    }
}
