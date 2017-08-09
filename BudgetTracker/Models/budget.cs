using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Models
{
    public abstract class BaseEntity { }
    public class Budget : BaseEntity
    {
        public int id { get; set; }

        public string name { get; set; }
        public  decimal amount_budget { get; set; }
        public List<Transaction> Transactions { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public Budget()
        {
            Transactions = new List<Transaction>();
        }
    }
}
