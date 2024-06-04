using MIS.Domain.Entities.Base;

namespace MIS.Domain.Entities
{
    public class Service : EntityBase
    {
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
