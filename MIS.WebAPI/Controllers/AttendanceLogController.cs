using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Application._Exceptions;
using MIS.Application.GuestAttendanceLogs.Commands.LogGuestAttendance;

namespace MIS.WebAPI.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class AttendanceLogController : ApiControllerBase
    {
        private readonly ILogger<AttendanceLogController> _logger;

        public AttendanceLogController(ILogger<AttendanceLogController> logger)
        {
            _logger = logger;
        }

        [HttpPost("log")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Log(LogAttendanceCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);

                return Ok(new { message = result });
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
