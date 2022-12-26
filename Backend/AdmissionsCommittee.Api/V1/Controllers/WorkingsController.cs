using AdmissionsCommittee.Contracts.V1.Request;
using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionsCommittee.Api.V1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WorkingsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdateEmployeeWorking(UpdateWorkingRequest[] request)
        //{
        //    var workings = await _unitOfWork.WorkingRepository.GetEmployeeWorkingsAsync(request.First().EmployeeId);
        //    if(workings.Count() < 1)
        //    {
        //        return NotFound();
        //    }
        //    var mappedWorkings = _mapper.Map(request, workings);
        //    var updatedWorkings = await _unitOfWork.WorkingRepository.UpdateManyAsync(mappedWorkings);
        //    var response = _mapper.Map<IEnumerable<WorkingResponse>>(updatedWorkings);

        //    return Ok(response);
        //}

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetAllById([FromRoute] int employeeId)
        {
            var workings = await _unitOfWork.WorkingRepository.GetEmployeeWorkingsAsync(employeeId);

            var response = _mapper.Map<IEnumerable<WorkingResponse>>(workings);

            return Ok(response);
        }
    }
}
