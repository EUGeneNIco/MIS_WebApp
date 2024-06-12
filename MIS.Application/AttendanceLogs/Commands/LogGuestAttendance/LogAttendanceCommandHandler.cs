using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.AttendanceLogs.Commands.LogGuestAttendance
{
    public class LogAttendanceCommandHandler : IRequestHandler<LogAttendanceCommand, string>
    {
        private readonly IAppDbContext dbContext;
        private readonly IRepository<Guest> guestRepository;

        public LogAttendanceCommandHandler(IAppDbContext dbContext, IRepository<Guest> guestRepository)
        {
            this.dbContext = dbContext;
            this.guestRepository = guestRepository;
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
            var member = await dbContext.Members
                .FirstOrDefaultAsync(x => !string.IsNullOrWhiteSpace(request.Code)
                    ? x.MemberCode == request.Code
                    : (x.Id == request.MemberId));

            if (member is null) return null;

            var logTime = DateTime.Now;

            var service = GetService(logTime);
            if (service is null)
            {
                // Save unindentified log
                dbContext.MemberAttendanceUnidentifiedLogs.Add(new MemberAttendanceUnidentifiedLog
                {
                    MemberId = member.Id,
                    LogDateTime = logTime
                });

                await dbContext.SaveChangesAsync(cancellationToken);
                return $"{member.FirstName} {member.LastName} is logged in. However, current time is out of service options so an admin needs to classify this.";
            }

            var log = new MemberAttendanceLog
            {
                MemberId = member.Id,
                LogDateTime = logTime,
                Service = service
            };

            dbContext.MemberAttendanceLogs.Add(log);
            await dbContext.SaveChangesAsync(cancellationToken);

            return $"{member.FirstName} {member.LastName} is successfully logged in.";
        }

        private async Task<string> LogGuest(LogAttendanceCommand request, CancellationToken cancellationToken)
        {
            var dbGuests = await guestRepository.GetAllAsync();
            var guest = dbGuests.FirstOrDefault(x => !string.IsNullOrWhiteSpace(request.Code)
                    ? x.Code == request.Code
                    : x.Id == request.GuestId);
            if (guest is null)
                throw new NotFoundException("Guest or Member not found.");

            var logTime = DateTime.Now;

            var service = GetService(logTime);
            if (service is null)
            {
                // Save unindentified log
                dbContext.GuestAttendanceUnidentifiedLogs.Add(new GuestAttendanceUnidentifiedLog
                {
                    GuestId = guest.Id,
                    LogDateTime = logTime
                });

                await dbContext.SaveChangesAsync(cancellationToken);
                return $"{guest.FirstName} {guest.LastName} is logged in. However, current time is out of service options so an admin needs to classify this.";
            }

            var log = new GuestAttendanceLog
            {
                GuestId = guest.Id,
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

            var service = dbContext.Services.FirstOrDefault(x => x.IsActive && TimeSpan.Compare(logTimeSpan, x.StartTime) >= 0 && TimeSpan.Compare(x.EndTime, logTimeSpan) >= 0);

            return service;
        }
    }
}
