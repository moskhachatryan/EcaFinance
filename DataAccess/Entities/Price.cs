using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal PresentAmount { get; set; }
    }
}
