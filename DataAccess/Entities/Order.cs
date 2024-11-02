using System;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace DataAccess.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public OrderType Type { get; set; }
        public int CategoryId { get; set; }
        public int Sort { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int ChurchServiceId { get; set; }
        [ForeignKey("ChurchServiceId")]
        public virtual ChurchService ChurchService { get; set; }
    }
}
