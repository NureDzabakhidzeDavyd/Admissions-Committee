using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Services
{
    public interface ISeeder
    {
        public Task SeedAsync();
    }
}
