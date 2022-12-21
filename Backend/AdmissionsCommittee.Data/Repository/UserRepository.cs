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
    public class UserRepository : BaseRepository<UserProfile>, IUserRepository
    {
        public UserRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder) : base(sqlConfiguration, queryBuilder) { }

        public async Task<UserProfile?> GetUserByEmail(string email)
        {
            var query = new Query(nameof(UserProfile)).Where(nameof(UserProfile.Email), "=", email);
            var sql = QueryBuilder.MsSqlQueryToString(query);
            var result = await Connection.QuerySingleOrDefaultAsync<UserProfile?>(sql, new { email });
            return result;
        }
    }
}
