using MediatR;

namespace MIS.Application.Members.Commands.CreateGuest
{
    public class CreateGuestCommand : IRequest<long>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? Age { get; set; }

        public int? NetworkId { get; set; }
    }
}
