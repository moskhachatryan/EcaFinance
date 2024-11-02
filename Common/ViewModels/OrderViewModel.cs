using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public OrderType Type { get; set; }
        public int CategoryId { get; set; }
        public int ChurchServiceId { get; set; }
        public bool IsDeleted { get; set; }
        public int OrderNumber { get; set; }
        public int Sort { get; set; }
        public CategoryViewModel Category { get; set; }
        public ChurchServiceViewModel ChurchService { get; set; }
        public string DisplayPriceCSS { get { return Type == OrderType.Debit ? "text-end" : ""; } }
    }
}
