using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceLogsGrid
{
    public class MemberAttendanceLogsGridItem : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Member { get; set; }
        public string Service { get; set; }
        public string LogDateTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MemberAttendanceLog, MemberAttendanceLogsGridItem>()
                .ForMember(dm => dm.LogDateTime, mo => mo.MapFrom(s => s.LogDateTime.ToString("dddd, MMM dd, yyyy hh:mm tt")))
                .ForMember(dm => dm.Member, mo => mo.MapFrom(s => $"{s.Member.LastName}, {s.Member.FirstName} {s.Member.MiddleName}"))
                .ForMember(dm => dm.Service, mo => mo.MapFrom(s => s.Service.Name));
        }
    }


}
