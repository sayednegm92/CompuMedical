using CompuMedical.Core.BaseEntities;

namespace CompuMedical.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public decimal? Taxes { get; set; }
        public Guid? StoreId { get; set; }
        public Store? Store { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
