using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Eie))]
    public class Eie
    {
        [Key]
        public int EieId { get; set; }
        public string EieName { get; set; }
    }
}
