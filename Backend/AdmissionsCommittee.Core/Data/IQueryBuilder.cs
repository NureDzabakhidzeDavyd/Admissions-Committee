using AdmissionsCommittee.Core.Domain.Filters;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Data
{
    public interface IQueryBuilder
    {
        public string PaginateFilter(PaginationFilter paginationFilter, SortFilter? sortFilter, DynamicFilters? dynamicFilters);
        public string MsSqlQueryToString(Query query);
        public Query GetAllQuery { get; set; }
        public string TableName { get; set; }
    }
}
