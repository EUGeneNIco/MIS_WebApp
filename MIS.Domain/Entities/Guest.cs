using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class Guest : EntityBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }

        public long NetworkId { get; set; }
        public virtual Network Network { get; set; }
    }
}
