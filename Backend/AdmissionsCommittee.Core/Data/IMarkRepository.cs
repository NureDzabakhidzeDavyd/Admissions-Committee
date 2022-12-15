using AdmissionsCommittee.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Data
{
    public interface IMarkRepository : IRepository<Mark>
    {
        public Task<IEnumerable<Mark>> GetApplicantMarks(int id);
    }
}
