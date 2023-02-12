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
        public UserRepository(DapperContext dapperContext, IQueryBuilder queryBuilder) : base(dapperContext, queryBuilder) { }

        public async Task<UserProfile?> GetUserByEmail(string email)
        {
            var query = new Query(nameof(UserProfile)).Where(nameof(UserProfile.Email), "=", email);
            var sql = QueryBuilder.MsSqlQueryToString(query);
            var result = await Connection.QuerySingleOrDefaultAsync<UserProfile?>(sql, new { email });
            return result;
        }

        public async Task<UserProfile> GetUserByCredentials(string userName, string password)
        {
            var query = new Query(nameof(UserProfile)).Where(new
            {
                FirstName = userName,
                Password = password,
            });
            var sql = QueryBuilder.MsSqlQueryToString(query);
            var user = await Connection.QuerySingleOrDefaultAsync<UserProfile>(sql);

            return user;
        }

        public async Task<UserProfile> GetByFirstName(string username)
        {
            var query = new Query(nameof(UserProfile)).Where(nameof(UserProfile.FirstName), "=", username);
            var sql = QueryBuilder.MsSqlQueryToString(query);
            var user = await Connection.QuerySingleOrDefaultAsync<UserProfile>(sql, new { username });
            return user;
        }

    }
}
