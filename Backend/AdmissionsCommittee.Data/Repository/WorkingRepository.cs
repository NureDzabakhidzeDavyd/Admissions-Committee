using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
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
    public class WorkingRepository : BaseRepository<Working>, IWorkingRepository
    {
        public WorkingRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder) : base(sqlConfiguration, queryBuilder)
        {
        }

        public async Task<IEnumerable<Working>> GetEmployeeWorkingsAsync(int employeeId)
        {
            var rankTableName = nameof(Rank);

            var sql = new Query(TableName)
                .Join(rankTableName, $"{rankTableName}.{nameof(Rank.RankId)}", $"{TableName}.{nameof(Working.RankId)}")
                .Where(nameof(Working.EmployeeId), "=", employeeId);
            var query = QueryBuilder.MsSqlQueryToString(sql);

            var workings = await Connection.QueryAsync<Working, Rank, Working>(query, (working, rank) =>
            {
                working.Rank = rank;
                return working;
            }, splitOn: nameof(Rank.RankId));

            return workings;
        }
    }
}
