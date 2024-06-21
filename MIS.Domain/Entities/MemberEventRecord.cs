using MIS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class MemberEventRecord  : EntityBase
    {
        public long MemberId { get; set; }
        public virtual Member Member { get; set; }

        public long EventId { get; set; }
        public virtual Event Event { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
