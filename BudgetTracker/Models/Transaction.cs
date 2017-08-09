using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Models
{
    public class Transaction : BaseEntity
    {
        public int id { get; set; }
        public decimal amount_trans { get; set; }
        public Budget budget_item { get; set; }
        public int budgetid { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public String description { get; set; } 
    }
}
