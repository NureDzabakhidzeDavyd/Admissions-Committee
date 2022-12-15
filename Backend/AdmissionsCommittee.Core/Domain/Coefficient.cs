using Dapper.Contrib.Extensions;

namespace AdmissionsCommittee.Core.Domain
{
    [Table(nameof(Coefficient))]
    public class Coefficient
    {
        [Key]
        public int CoefficientId { get; set; }
        public int SpecialityId { get; set; }
        public float CoefficientValue { get; set; }
        public int EieId { get; set; }
        [Write(false)]
        public Eie Eie { get; set; }

    }
}
