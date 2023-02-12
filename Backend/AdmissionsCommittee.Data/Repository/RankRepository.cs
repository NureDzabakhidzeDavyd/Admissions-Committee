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
    public class RankRepository : BaseRepository<Rank>, IRankRepository
    {
        public RankRepository(DapperContext dapperContext, IQueryBuilder queryBuilder) : base(dapperContext, queryBuilder)
        {
        }
    }
}
