using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class GuestEventRecord : EntityBase
    {
        public long GuestId { get; set; }
        public virtual Guest Guest { get; set; }

        public long EventId { get; set; }
        public virtual Event Event { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
