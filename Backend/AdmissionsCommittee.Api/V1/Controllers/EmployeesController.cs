using AdmissionsCommittee.Contracts.V1.Request.Employee;
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

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="request">Employee information</param>
        /// <returns>New employee</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
        {
            var employee = _mapper.Map<Employee>(request);

            var person = await _unitOfWork.PersonRepository.CreateAsync(employee.Person);

            employee.EmployeeId = person.PersonId;
            employee = await _unitOfWork.EmployeeRepository.CreateAsync(employee);

            employee.Working.ToList().ForEach(working => working.EmployeeId = employee.EmployeeId);
            var workings = await _unitOfWork.WorkingRepository.CreateManyAsync(employee.Working);

            employee.Person = person;
            employee.Working = (List<Working>)workings;

            var faculty = await _unitOfWork.FacultyRepository.GetByIdAsync(employee.FacultyId);
            employee.Faculty = faculty;

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

            var newPerson = await _unitOfWork.PersonRepository.UpdateAsync(person);
            var newEmployee = await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
            newEmployee.Faculty = await _unitOfWork.FacultyRepository.GetByIdAsync(employee.FacultyId);

            newEmployee.Person = newPerson;

            var response = _mapper.Map<EmployeeResponse>(newEmployee);

            return Ok(newEmployee);
        }

        [HttpDelete("{employeeId:int}")]
        public async Task<IActionResult> DeleteEmployeeById([FromRoute] int employeeId)
        {
             var person = await _unitOfWork.PersonRepository.GetByIdAsync(employeeId);
            if(person is null)
            {
                return NotFound();
            }

            await _unitOfWork.PersonRepository.DeleteByIdAsync(person.PersonId);
            return NoContent();
        }
    }
}
