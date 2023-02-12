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
    public class MarkRepository : BaseRepository<Mark>, IMarkRepository
    {
        public MarkRepository(DapperContext dapperContext, IQueryBuilder queryBuilder) : base(dapperContext, queryBuilder)
        {
        }

        public async Task<IEnumerable<Mark>> GetApplicantMarks(int id)
        {
            var eieTableName = nameof(Eie);

            var sql = new Query(TableName).Where(nameof(Mark.ApplicantId), "=", id)
                .Join(eieTableName, $"{eieTableName}.{nameof(Eie.EieId)}", $"{TableName}.{nameof(Mark.EieId)}");
            var query = QueryBuilder.MsSqlQueryToString(sql);

            var marks = await Connection.QueryAsync<Mark, Eie, Mark>(query, (mark, eie) =>
            {
                mark.Eie = eie;
                return mark;
            }, splitOn: nameof(Eie.EieId));

            return marks;
        }
    }
}
