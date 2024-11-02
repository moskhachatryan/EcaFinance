using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class BalanceLimit
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Limit{ get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }
        public int? ChurchServiceId { get; set; }
        [ForeignKey("ChurchServiceId")]
        public virtual ChurchService ChurchService { get; set; }
    }
}
