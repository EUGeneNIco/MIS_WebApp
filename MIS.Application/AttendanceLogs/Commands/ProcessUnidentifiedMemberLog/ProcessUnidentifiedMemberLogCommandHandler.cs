using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Domain;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.AttendanceLogs.Commands.ProcessUnidentifiedMemberLog
{
    public class ProcessUnidentifiedMemberLogCommandHandler : IRequestHandler<ProcessUnidentifiedMemberLogCommand, Unit>
    {
        private readonly IAppDbContext dbContext;

        public ProcessUnidentifiedMemberLogCommandHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(ProcessUnidentifiedMemberLogCommand request, CancellationToken cancellationToken)
        {
            var service = await dbContext.Services
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.IsActive && x.Id == request.ServiceId);
            if (service is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Service"));

            var log = await dbContext.MemberAttendanceUnidentifiedLogs
                .FirstOrDefaultAsync(x => x.Id == request.UnidentifiedLogId);
            if (log is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Member Unidentified Log"));

            // Insert Attendance Log
            var goodLog = new MemberAttendanceLog
            {
                Service = service,
                Member = log.Member,
                LogDateTime = log.LogDateTime,
            };

            dbContext.MemberAttendanceLogs.Add(goodLog);

            // Delete identified log
            dbContext.MemberAttendanceUnidentifiedLogs.Remove(log);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
