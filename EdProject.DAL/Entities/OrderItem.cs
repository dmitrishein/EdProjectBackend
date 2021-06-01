using EdProject.DAL.Entities.Enums;

namespace EdProject.DAL.Entities
{
    public class OrderItem
    {
        public long OrderId { get; set; }
        public virtual Orders Order { get; set; }

        public long EditionId { get; set; }
        public virtual Edition Edition { get; set; }

        public int ItemsCount { get; set; }

    }
}
