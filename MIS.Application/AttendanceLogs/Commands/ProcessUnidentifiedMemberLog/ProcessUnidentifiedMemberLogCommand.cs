using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.AttendanceLogs.Commands.ProcessUnidentifiedMemberLog
{
    public class ProcessUnidentifiedMemberLogCommand : IRequest<Unit>
    {
        public long ServiceId { get; set; }
        public long UnidentifiedLogId { get; set; }
    }
}
