using AdmissionsCommittee.Contracts.V1.Request;
using AdmissionsCommittee.Contracts.V1.Request.Applicant;
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
    public class ApplicantsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicantsController(IMapper mapper, IUnitOfWork unitOfWork)
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
            var applicant = await _unitOfWork.ApplicantRepository.PaginateAsync(paginationFilter, sortFilter, dynamicFilters);
            var response = _mapper.Map<IEnumerable<ApplicantResponse>>(applicant);
            return Ok(response);
        }

        /// <summary>
        /// Get applicant information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid([FromRoute] int id)
        {
            var applicant = await _unitOfWork.ApplicantRepository.GetByIdAsync(id);
            if (applicant is null)
            {
                return NotFound();
            }
            var response = _mapper.Map<ApplicantResponse>(applicant);
            return Ok(response);
        }

        // TODO: Test this code
        /// <summary>
        /// Create new applicant with marks
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// <br /> {
        /// <br />   "person": {
        /// <br />          "firstName": "Степан",
        /// <br />          "secondName": "Андрі́йович",
        /// <br />          "patronymic": "Банде́ра",
        /// <br />          "address": "с. Старий Угринів,Калуській район, Івано-Франківська область",
        /// <br />          "birth": "1959-10-15",
        /// <br />          "email": "banderapapa@gmail.com",
        /// <br />          "phone": "+19391945"
        /// <br />   },
        /// <br />   "marks": [
        /// <br />     {
        /// <br />       "eieId": 16,
        /// <br />          "markValue": 199,
        /// <br />          "writeYear": 2018
        /// <br />     },
        /// <br />     {
        /// <br />      "eieId": 15,
        /// <br />          "markValue": 198,
        /// <br />          "writeYear": 2018
        /// <br />     },
        /// <br />     {
        /// <br />      "eieId": 14,
        /// <br />          "markValue": 197,
        /// <br />          "writeYear": 2018
        /// <br />     }
        /// <br />   ],
        /// <br />          "certificate": 0.9
        /// <br /> }  
        /// <br />  </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(ApplicantResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateApplicantRequest request)
        {
            var applicant = _mapper.Map<Applicant>(request);

            var person = await _unitOfWork.PersonRepository.CreateAsync(applicant.Person);

            applicant.Person = person;
            applicant.ApplicantId = person.PersonId;
            applicant = await _unitOfWork.ApplicantRepository.CreateAsync(applicant);

            var marks = _mapper.Map<IEnumerable<Mark>>(request.Marks);
            marks.ToList().ForEach(x => x.ApplicantId = applicant.ApplicantId);
             await _unitOfWork.MarkRepository.CreateManyAsync(marks);
          
            var response = _mapper.Map<ApplicantResponse>(applicant);
            return Ok(response);
        }

        /// <summary>
        /// Get all statements by applicantId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/statements")]
        public async Task<IActionResult> GetApplicantStatements
            ([FromRoute] int id)
        {
            var statements = await _unitOfWork.StatementRepository.GetApplicantStatementsAsync(id);
            if (!statements.Any())
            {
                return NotFound();
            }
            var response = _mapper.Map<IEnumerable<StatementResponse>>(statements);
            return Ok(response);
        }

        [HttpGet("{id}/marks")]
        public async Task<IActionResult> GetApplicantMarks
            ([FromRoute] int id)
        {
            var marks = await _unitOfWork.MarkRepository.GetApplicantMarks(id);
            if (!marks.Any())
            {
                return NotFound();
            }
            var response = _mapper.Map<IEnumerable<MarkResponse>>(marks);
            return Ok(response);
        }

        /// <summary>
        /// Update applicant information
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{applicantId:int}")]
        public async Task<IActionResult> Update([FromRoute] int applicantId, [FromBody] UpdateApplicantRequest request)
        {
            var applicant = await _unitOfWork.ApplicantRepository.GetByIdAsync(applicantId);
            if (applicant is null)
            {
                return NotFound();
            }
            applicant = _mapper.Map(request, applicant);

            var person = await _unitOfWork.PersonRepository.GetByIdAsync(applicantId);
            if (person is null)
            {
                return NotFound();
            }
            person = _mapper.Map(request.Person, person);

            var newPerson = await _unitOfWork.PersonRepository.UpdateAsync(person);
            var newApplicant = await _unitOfWork.ApplicantRepository.UpdateAsync(applicant);
            newApplicant.Person = newPerson;

            var response = _mapper.Map<ApplicantResponse>(newApplicant);

            return Ok(newApplicant);
        }

        /// <summary>
        /// Delete applicant by id
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        [HttpDelete("{employeeId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteApplicantById([FromRoute] int applicantId)
        {
            var applicant = await _unitOfWork.ApplicantRepository.GetByIdAsync(applicantId);
            if (applicant is null)
            {
                return NotFound();
            }

            await _unitOfWork.EmployeeRepository.DeleteByIdAsync(applicant.ApplicantId);
            return NoContent();
        }
    }
}
