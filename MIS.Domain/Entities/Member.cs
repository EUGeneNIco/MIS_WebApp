using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class Member : EntityBase
    {
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public string Extension { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string NetworkImported { get; set; } // we need this for importing data in excel, all data are in string or text
        public long? NetworkId { get; set; }
        public virtual Network Network { get; set; }
        public DateTime? ImportDate { get; set; }
        public string Status { get; set; }
        public bool IsWorker { get; set; }
    }
}
