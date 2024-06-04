using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class MemberAttendanceLog : IEntity
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public virtual Member Member { get; set; }
        public DateTime LogDateTime { get; set; }
        public long ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
