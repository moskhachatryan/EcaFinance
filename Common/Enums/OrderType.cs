using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum OrderType
    {
        [Display(Name = "Ելք")]
        Debit = 1,
        [Display(Name = "Մուտք")]
        Deposit = 2
    }
}
