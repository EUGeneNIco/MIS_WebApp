using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Services.Queries;

namespace MIS.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : ApiControllerBase
    {
        [HttpGet("getServices")]
        public async Task<ActionResult> GetServices()
        {
            try
            {
                var data = await Mediator.Send(new GetServicesQuery());

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
