using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public DateTime? LastSuccessfulLogin { get; set; }
        public DateTime? LastFailedLoginAttempt { get; set; }
        public string Role { get; set; }
        public int FailedLogInAttempt { get; set; }
    }
}
