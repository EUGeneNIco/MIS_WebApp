using MediatR;

namespace MIS.Application.Members.Commands.ImportMemberData
{
    public class ImportMemberDataCommand : IRequest<Unit>
    {
        public List<MemberData> ImportedData { get; set; }
    }

    public class MemberData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? Age { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Category { get; set; }
        public string MemberCode { get; set; }
        public string Extension { get; set; }
        public string NetworkImported { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string Barangay { get; set; }
    }
}
