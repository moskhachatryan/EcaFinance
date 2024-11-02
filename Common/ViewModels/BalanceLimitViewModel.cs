using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.ViewModels
{
    public class BalanceLimitViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Limit { get; set; }
        public decimal SpentMoney { get; set; }
        public int Percent { get; set; }
        public string PercentBgCss { 
            get {
                return Percent < 50 ? "bg-success" : Percent < 75  ? "bg-warning" : "bg-danger";
            }
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CategoryViewModel Category { get; set; }
        public int? ChurchServiceId { get; set; }
        public ChurchServiceViewModel ChurchService { get; set; }
    }
}
