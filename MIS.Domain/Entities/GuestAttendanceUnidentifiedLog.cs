using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class GuestAttendanceUnidentifiedLog : IEntity
    {
        public long Id { get; set; }
        public long GuestId { get; set; }
        public virtual Guest Guest { get; set; }
        public DateTime LogDateTime { get; set; }
        public DateTime? SettledDate { get; set; }
    }
}
