using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Domain.Filters;
using AdmissionsCommittee.Core.Options;
using Dapper;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Data.Repository
{
    public class StatisticRepository : BaseRepository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder) : base(sqlConfiguration, queryBuilder)
        {
        }

        public async Task<IEnumerable<Statistic>> GetAllSpecialityStatisticsAsync(PaginationFilter paginationFilter, 
            SortFilter? sortFilter, 
            DynamicFilters? dynamicFilters, 
            int specialityId)
        {
            QueryBuilder.GetAllQuery = new Query(TableName).Where(nameof(Statistic.SpecialityId), "=", specialityId);
            QueryBuilder.TableName = nameof(TableName);

            var query = QueryBuilder.PaginateFilter(paginationFilter, sortFilter, dynamicFilters);
            var statistics = await Connection.QueryAsync<Statistic>(query);
            return statistics;
        }

        public async override Task<IEnumerable<Statistic>> PaginateAsync(PaginationFilter paginationFilter, SortFilter? sortFilter, DynamicFilters? dynamicFilters)
        {
            QueryBuilder.GetAllQuery = new Query(TableName);
            QueryBuilder.TableName = nameof(Employee);

            var query = QueryBuilder.PaginateFilter(paginationFilter, sortFilter, dynamicFilters);
            var statistics = await Connection.QueryAsync<Statistic>(query);
            return statistics;
        }
    }
}
