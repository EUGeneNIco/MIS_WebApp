using MediatR;
using MIS.Application.Dashboards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Dashboards.Queries.GetAttendanceData
{
    public class GetAttendanceDataQuery : IRequest<AttendanceDataResponse>
    {
    }
}
