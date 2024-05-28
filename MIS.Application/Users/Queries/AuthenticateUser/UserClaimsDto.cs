using System.Collections.Generic;
using System.Security.Claims;

namespace MIS.Application.Users.Queries.AuthenticateUser
{
    public class UserClaimsDto
    {
        public long UserGuid { get; set; }

        public string DisplayName { get; set; }

        public string Role { get; set; }
    }
}
