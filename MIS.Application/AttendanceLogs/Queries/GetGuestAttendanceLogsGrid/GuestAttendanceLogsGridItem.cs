using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.AttendanceLogs.Queries.GetGuestAttendanceLogsGrid
{
    public class GuestAttendanceLogsGridItem : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Service { get; set; }
        public string LogDateTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GuestAttendanceLog, GuestAttendanceLogsGridItem>()
                .ForMember(dm => dm.LogDateTime, mo => mo.MapFrom(s => s.LogDateTime.ToString("dddd, MMM dd, yyyy hh:mm tt")))
                .ForMember(dm => dm.Name, mo => mo.MapFrom(s => $"{s.Guest.LastName}, {s.Guest.FirstName} {s.Guest.MiddleName}"))
                .ForMember(dm => dm.Service, mo => mo.MapFrom(s => s.Service.Name));
        }
    }


}
