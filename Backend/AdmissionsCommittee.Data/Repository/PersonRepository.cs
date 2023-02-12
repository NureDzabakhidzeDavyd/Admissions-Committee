using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Data.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(DapperContext dapperContext, IQueryBuilder queryBuilder) : base(dapperContext, queryBuilder)
        {
        }
    }
}
