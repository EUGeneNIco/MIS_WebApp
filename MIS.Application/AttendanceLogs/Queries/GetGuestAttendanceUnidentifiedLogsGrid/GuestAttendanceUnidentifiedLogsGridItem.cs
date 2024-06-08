using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.AttendanceLogs.Queries.GetGuestAttendanceUnidentifiedLogsGrid
{
    public class GuestAttendanceUnidentifiedLogsGridItem : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LogDateTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GuestAttendanceUnidentifiedLog, GuestAttendanceUnidentifiedLogsGridItem>()
                .ForMember(dm => dm.LogDateTime, mo => mo.MapFrom(s => s.LogDateTime.ToString("dddd, MMM dd, yyyy hh:mm tt")))
                .ForMember(dm => dm.Name, mo => mo.MapFrom(s => $"{s.Guest.LastName}, {s.Guest.FirstName} {s.Guest.MiddleName}"));
        }
    }


}
