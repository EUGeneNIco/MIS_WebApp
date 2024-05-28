using System.Threading.Tasks;

namespace MIS.WebAPI.Auth
{
    public interface IJwtAuthenticationManager
    {
        Task<string> Authenticate(UserCredentials creds);

        Task<bool> ValidateTokenAsync(string token);
    }
}
