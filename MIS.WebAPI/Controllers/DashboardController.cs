using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Dashboards.Queries.GetAttendanceData;

namespace MIS.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : ApiControllerBase
    {
        [HttpGet("getAttendanceData")]
        public async Task<ActionResult> GetAttendanceData()
        {
            try
            {
                var data = await Mediator.Send(new GetAttendanceDataQuery());

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
