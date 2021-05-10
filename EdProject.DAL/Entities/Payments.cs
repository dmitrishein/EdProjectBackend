using EdProject.DAL.Entities.Base;

namespace EdProject.DAL.Entities
{
    public class Payments:BaseEntity
    {
        public string TransactionId { get; set; }
        public virtual Orders Order { get; set; }
    }
}
