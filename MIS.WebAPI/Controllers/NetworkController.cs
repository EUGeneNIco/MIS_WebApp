using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Application._Exceptions;
using MIS.Application.Networks.Queries.GetNetworks;

namespace MIS.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NetworkController : ApiControllerBase
    {
        private readonly ILogger<NetworkController> _logger;

        public NetworkController(ILogger<NetworkController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await Mediator.Send(new GetNetworksQuery());

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
