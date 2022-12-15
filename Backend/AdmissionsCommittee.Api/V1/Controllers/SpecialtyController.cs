using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Domain.Filters;

namespace AdmissionsCommittee.Api.V1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SpecialityController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter,
            [FromQuery] SortFilter? sortFilter = null,
            [FromQuery] DynamicFilters? dynamicFilters = null)
        {
            var specialty = await _unitOfWork.SpecialtyRepository.PaginateAsync(paginationFilter, sortFilter, dynamicFilters);
            var response = _mapper.Map<IEnumerable<SpecialtyResponse>>(specialty);
            return Ok(response);
        }

        [HttpGet("{id}/statements")]
        public async Task<IActionResult> GetStatementsBySpecialityId
            ([FromRoute] int id,
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] SortFilter? sortFilter = null,
            [FromQuery] DynamicFilters? dynamicFilters = null)
        {
            var statements = await _unitOfWork.StatementRepository.GetAllSpecialityStatementsAsync(id);
            if(!statements.Any())
            {
                return NotFound();
            }
            var response = _mapper.Map<IEnumerable<StatementResponse>>(statements);
            return Ok(response);
        }

        [HttpGet("{id}/statistics")]
        public async Task<IActionResult> GetAllSpecialityStatistics([FromRoute] int id, 
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] SortFilter? sortFilter = null,
            [FromQuery] DynamicFilters? dynamicFilters = null)
        {
            var statistics = await _unitOfWork.StatisticRepository
                .GetAllSpecialityStatisticsAsync(paginationFilter, sortFilter, dynamicFilters, id);
            if (!statistics.Any())
            {
                return NotFound();
            }
            var response = _mapper.Map<IEnumerable<StatisticResponse>>(statistics);
            return Ok(response);
        }

        [HttpGet("{id}/coefficients")]
        public async Task<IActionResult> GetAllSpecialityCoefficients([FromRoute] int id)
        {
            var coeffs = await _unitOfWork.CoefficientRepository.GetAllSpecialityCoefficientsAsync(id);
            if (!coeffs.Any())
            {
                return NotFound();
            }
            var response = _mapper.Map<IEnumerable<CoefficientResponse>>(coeffs);
            return Ok(response);
        }


        [HttpGet("{id}/compare-competitive/{competitiveScore}")]
        public async Task<IActionResult> CompareApplicantCompetitiveScore([FromRoute] int id, [FromRoute] int competitiveScore)
        {
            var applicantsCompetitiveScores = await _unitOfWork.SpecialtyRepository.CompareApplicantCompetitiveScore(competitiveScore, id);

            var response = _mapper.Map<CompetitiveScoreStatisticResponse>(applicantsCompetitiveScores);
            return Ok(response);
        }
    }
}
