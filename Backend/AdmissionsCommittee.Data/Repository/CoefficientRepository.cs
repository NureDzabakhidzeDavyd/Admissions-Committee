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
    public class CoefficientRepository : BaseRepository<Coefficient>, ICoefficientRepository
    {
        public CoefficientRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder) : base(sqlConfiguration, queryBuilder)
        {
        }

        public async Task<IEnumerable<Coefficient>> GetAllSpecialityCoefficientsAsync(int id)
        {
            var eieTablename = nameof(Eie);
            var sql = new Query(TableName)
                .Where(nameof(Coefficient.SpecialityId), "=", id)
                .Join(eieTablename, $"{eieTablename}.{nameof(Eie.EieId)}",
                    $"{TableName}.{nameof(Coefficient.EieId)}");
            var query = QueryBuilder.MsSqlQueryToString(sql);

            var coeffs = await Connection.QueryAsync<Coefficient, Eie, Coefficient>(query, (coef, eie)=>
            {
                coef.Eie = eie;
                coef.EieId = eie.EieId;
                return coef;
            }, splitOn: $"{nameof(Eie.EieId)}");

            return coeffs;
        }
    }
}
