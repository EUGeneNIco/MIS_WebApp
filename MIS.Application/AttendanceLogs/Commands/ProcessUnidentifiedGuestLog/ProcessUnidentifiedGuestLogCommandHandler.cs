using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.AttendanceLogs.Commands.ProcessUnidentifiedGuestLog
{
    public class ProcessUnidentifiedGuestLogCommandHandler : IRequestHandler<ProcessUnidentifiedGuestLogCommand, Unit>
    {
        private readonly IAppDbContext dbContext;

        public ProcessUnidentifiedGuestLogCommandHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(ProcessUnidentifiedGuestLogCommand request, CancellationToken cancellationToken)
        {
            var service = await dbContext.Services
                .FirstOrDefaultAsync(x => x.IsActive && x.Id == request.ServiceId);
            if (service is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Service"));

            var log = await dbContext.GuestAttendanceUnidentifiedLogs
                .FirstOrDefaultAsync(x => x.Id == request.UnidentifiedLogId);
            if (log is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest Unidentified Log"));

            // Insert Attendance Log
            var attendanceLog = new GuestAttendanceLog
            {
                Service = service,
                Guest = log.Guest,
                LogDateTime = log.LogDateTime,
            };

            dbContext.GuestAttendanceLogs.Add(attendanceLog);

            // Delete identified log
            dbContext.GuestAttendanceUnidentifiedLogs.Remove(log);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
