using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Application._Exceptions;
using MIS.WebAPI.Auth;

namespace MIS.WebAPI.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        private readonly IJwtAuthenticationManager authenticationManager;

        public AuthController(IJwtAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserCredentials creds)
        {
            try
            {
                var result = await authenticationManager.Authenticate(creds);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
