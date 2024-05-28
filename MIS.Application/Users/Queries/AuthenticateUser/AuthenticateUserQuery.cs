using MediatR;

namespace MIS.Application.Users.Queries.AuthenticateUser
{
    public class AuthenticateUserQuery : IRequest<UserClaimsDto>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
