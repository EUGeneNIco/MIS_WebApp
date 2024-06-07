using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Application._Exceptions;
using MIS.Application.AttendanceLogs.Commands.LogGuestAttendance;
using MIS.Application.AttendanceLogs.Commands.ProcessUnidentifiedMemberLog;
using MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceLogsGrid;
using MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceUnidentifiedLogsGrid;
using MIS.Application.AttendanceLogs.Queries.GetMemberGuest;

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

        [HttpPost("getQuery")]
        public async Task<IActionResult> GetQuery(GetMemberGuestQuery command)
        {
            try
            {
                var result = await Mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("getMemberAttendanceLogs")]
        public async Task<ActionResult> GetMemberAttendanceLogs([FromBody] GetMemberAttendanceLogsGridQuery query)
        {
            try
            {
                var data = await Mediator.Send(query);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("getMemberAttendanceUnidentifiedLogs")]
        public async Task<ActionResult> GetMemberAttendanceUnidentifiedLogs([FromBody] GetMemberAttendanceUnidentifiedLogsGridQuery query)
        {
            try
            {
                var data = await Mediator.Send(query);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("processMemberUnidentifiedLog")]
        public async Task<ActionResult> ProcessMemberUnidentifiedLog([FromBody] ProcessUnidentifiedMemberLogCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);

                return Ok(data);
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
