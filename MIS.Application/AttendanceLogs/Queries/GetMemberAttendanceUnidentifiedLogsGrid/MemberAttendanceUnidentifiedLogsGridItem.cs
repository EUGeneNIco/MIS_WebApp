using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceUnidentifiedLogsGrid
{
    public class MemberAttendanceUnidentifiedLogsGridItem : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Member { get; set; }
        public string LogDateTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MemberAttendanceUnidentifiedLog, MemberAttendanceUnidentifiedLogsGridItem>()
                .ForMember(dm => dm.LogDateTime, mo => mo.MapFrom(s => s.LogDateTime.ToString("dddd, MMM dd, yyyy hh:mm tt")))
                .ForMember(dm => dm.Member, mo => mo.MapFrom(s => $"{s.Member.LastName}, {s.Member.FirstName} {s.Member.MiddleName}"));
        }
    }


}
