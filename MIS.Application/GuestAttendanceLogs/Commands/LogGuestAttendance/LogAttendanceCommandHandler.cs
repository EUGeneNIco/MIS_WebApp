using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Exceptions;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.GuestAttendanceLogs.Commands.LogGuestAttendance
{
    public class LogAttendanceCommandHandler : IRequestHandler<LogAttendanceCommand, string>
    {
        private readonly IAppDbContext dbContext;

        public LogAttendanceCommandHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Handle(LogAttendanceCommand request, CancellationToken cancellationToken)
        {
            var message = await LogMember(request, cancellationToken);

            if (string.IsNullOrEmpty(message))
                message = await LogGuest(request, cancellationToken);

            return message;
        }

        private async Task<string> LogMember(LogAttendanceCommand request, CancellationToken cancellationToken)
        {
            var member = await dbContext.Members.FirstOrDefaultAsync(x => x.MemberCode == request.Code && !x.IsDeleted);
            if (member == null) return null;

            var logTime = DateTime.Now;

            var service = GetService(logTime);
            if (service == null)
            {
                dbContext.MemberAttendanceUnidentifiedLogs.Add(new MemberAttendanceUnidentifiedLog
                {
                    Member = member,
                    LogDateTime = logTime
                });

                await dbContext.SaveChangesAsync(cancellationToken);
                return $"{member.FirstName} {member.LastName} is logged in. However, current time is out of service options so an admin needs to classify this.";
            }

            var log = new MemberAttendanceLog
            {
                Member = member,
                LogDateTime = logTime,
                Service = service
            };

            dbContext.MemberAttendanceLogs.Add(log);
            await dbContext.SaveChangesAsync(cancellationToken);

            return $"{member.FirstName} {member.LastName} is successfully logged in.";
        }

        private async Task<string> LogGuest(LogAttendanceCommand request, CancellationToken cancellationToken)
        {
            var guest = await dbContext.Guests.FirstOrDefaultAsync(x => x.Code == request.Code && !x.IsDeleted);
            if (guest == null)
                throw new NotFoundException("Guest not found.");

            var logTime = DateTime.Now;

            var service = GetService(logTime);
            if (service == null)
            {
                await SaveUnidentifiedGuestAttendanceLog(guest, logTime, cancellationToken);
                return $"{guest.FirstName} {guest.LastName} is logged in. However, current time is out of service options so an admin needs to classify this.";
            }

            var log = new GuestAttendanceLog
            {
                Guest = guest,
                LogDateTime = logTime,
                Service = service
            };

            dbContext.GuestAttendanceLogs.Add(log);
            await dbContext.SaveChangesAsync(cancellationToken);

            return $"{guest.FirstName} {guest.LastName} is successfully logged in.";
        }

        private Service? GetService(DateTime logTime)
        {
            //logTime = new DateTime(2024, 03, 03, 7, 12, 0);
            var logTimeSpan = logTime.TimeOfDay;

            var service = dbContext.Services.FirstOrDefault(x => x.IsActive && !x.IsDeleted && TimeSpan.Compare(logTimeSpan, x.StartTime) >= 0 &&  TimeSpan.Compare(x.EndTime, logTimeSpan) >= 0);

            return service;
        }

        private async Task SaveUnidentifiedGuestAttendanceLog(Guest guest, DateTime logDateTime, CancellationToken cancellationToken)
        {
            dbContext.GuestAttendanceUnidentifiedLogs.Add(new GuestAttendanceUnidentifiedLog
            {
                Guest = guest,
                LogDateTime = logDateTime
            });

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
