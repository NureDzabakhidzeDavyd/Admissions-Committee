using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Data
{
    public interface IStatisticRepository : IRepository<Statistic>
    {
        public Task<IEnumerable<Statistic>> GetAllSpecialityStatisticsAsync(PaginationFilter paginationFilter,
            SortFilter? sortFilter,
            DynamicFilters? dynamicFilters,
            int specialityId);
    }
}
