using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class MemberAttendanceUnidentifiedLog : IEntity
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public virtual Member Member { get; set; }
        public DateTime LogDateTime { get; set; }
        public DateTime? SettledDate { get; set; }
    }
}
