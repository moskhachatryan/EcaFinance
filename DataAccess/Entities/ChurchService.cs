using System;

namespace DataAccess.Entities
{
    public class ChurchService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
