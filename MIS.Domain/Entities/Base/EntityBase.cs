namespace MIS.Domain.Entities.Base
{
    public abstract class EntityBase : IEntity, IAuditEntity
    {
        public long Id { get; set; }

        public long? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? ModifiedById { get; set; }
        public virtual User ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public T ShallowCopy<T>() where T : EntityBase
        {
            return (T)MemberwiseClone();
        }
    }
}
