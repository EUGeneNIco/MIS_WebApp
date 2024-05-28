using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Exceptions;
using MIS.Application._Helpers;
using MIS.Domain;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Users.Queries.AuthenticateUser
{
    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, UserClaimsDto>
    {
        private readonly IAppDbContext dbContext;

        public AuthenticateUserQueryHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserClaimsDto> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

            if (user is null)
                throw new UnauthorizedException("Wrong credentials.");

            if (user.PasswordHash == PasswordHelper.Hash(request.Password))
            {
                var successfulLogin = DateTime.Now;
                user.LastSuccessfulLogin = successfulLogin;
                user.FailedLogInAttempt = 0;

                await dbContext.SaveChangesAsync(cancellationToken);

                var userClaims = new UserClaimsDto
                {
                    UserGuid = user.Id,
                    DisplayName = $"{user.FirstName} {user.LastName}",
                    Role = user.Role
                };

                return userClaims;
            }
            else
            {
                user.FailedLogInAttempt++;
                user.LastFailedLoginAttempt = DateTime.Now;

                await dbContext.SaveChangesAsync(cancellationToken);

                throw new UnauthorizedException("Wrong credentials.");
            }
        }
    }
}
