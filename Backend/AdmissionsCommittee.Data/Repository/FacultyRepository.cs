using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdmissionsCommittee.Core.Options;
using AdmissionsCommittee.Data.Helpers;

namespace AdmissionsCommittee.Data.Repository
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder) : base(sqlConfiguration, queryBuilder)
        {
            queryBuilder.TableName = nameof(Faculty);
        }
    }
}
