using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Models
{
    public class BudgetTrackerContext : DbContext
    {
        public BudgetTrackerContext(DbContextOptions<BudgetTrackerContext> options) : base(options)
        { }
        
        public DbSet<Budget> Budgetitems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
