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
            // TODO: DynamicFilters have never work
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] SortFilter? sortFilter = null,
            [FromQuery] DynamicFilters? dynamicFilters = null)
        {
            var applicant = await _unitOfWork.ApplicantRepository.PaginateAsync(paginationFilter, sortFilter, dynamicFilters);
            var response = _mapper.Map<IEnumerable<ApplicantResponse>>(applicant);
            return Ok(response);
        }

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

        [HttpGet("{id}/competitive-score")]
        public async Task<IActionResult> GetApplicantCompetitiveScore
           ([FromRoute] int id, [FromQuery] int specialityId)
        {
            var competitiveScore = await _unitOfWork.ApplicantRepository.CalculateApplicantCompetitiveScore(id, specialityId);
            return Ok(competitiveScore);
        }

        [HttpGet("competitive-score")]
        public async Task<IActionResult> GetCompetitiveScore(
            [FromQuery] IEnumerable<ApplicantMarkRequest> applicantMarks,
            [FromQuery] int specialityId)
        {
            var result = 0f;
            applicantMarks = new[]
            {
                new ApplicantMarkRequest {EieId = 1, MarkValue=152},
                new ApplicantMarkRequest {EieId = 3, MarkValue=162},
                new ApplicantMarkRequest {EieId = 7, MarkValue=172},
            };

            var specialityCoeffs = await _unitOfWork.CoefficientRepository.GetAllSpecialityCoefficientsAsync(specialityId);
            
            foreach (var coef in specialityCoeffs)
            {
                var markValue = applicantMarks.Where(x => x.EieId == coef.EieId).First().MarkValue;
                if(markValue == 0)
                {
                    return NotFound($"Mark value for eie doesn't exist");
                }
                result += MathF.Floor(markValue * coef.CoefficientValue);
            }

            return Ok($"Your competitive score: {result}");
        }
    }
}
