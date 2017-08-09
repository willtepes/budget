using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudgetTracker.Models;

namespace BudgetTracker.Controllers
{
    public class BudgetController : Controller
    {
        private readonly BudgetTrackerContext _context;

        public BudgetController(BudgetTrackerContext context)
        {
            _context = context;    
        }

        // GET: Budget
        public async Task<IActionResult> Index()
        {
            return View(await _context.Budgetitems
                                .Include(Budgetitems => Budgetitems.Transactions)                       
                                .ToListAsync());
        }

        // GET: Budget/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgetitems
                                .Include(Budgetitems => Budgetitems.Transactions)                               
                                .SingleOrDefaultAsync(m => m.id == id);
            if (budget == null)
            {
                return NotFound();
            }

            foreach (var transaction in budget.Transactions)
            {
                budget.Transactions = budget.Transactions.OrderByDescending(c => c.created_at).ToList();
            }
            return View(budget);
        }

        // GET: Budget/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Budget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,amount_budget")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                budget.created_at = DateTime.Now;
                budget.updated_at = DateTime.Now;
                _context.Add(budget);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budget);
        }

        // GET: Budget/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgetitems.SingleOrDefaultAsync(m => m.id == id);
            if (budget == null)
            {
                return NotFound();
            }
            return View(budget);
        }

        // POST: Budget/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,amount_budget,created_at,updated_at")] Budget budget)
        {
            if (id != budget.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    budget.updated_at = DateTime.Now;
                    _context.Update(budget);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(budget);
        }

        // GET: Budget/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgetitems
                .SingleOrDefaultAsync(m => m.id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var budget = _context.Budgetitems.SingleOrDefault(m => m.id == id);
            _context.Budgetitems.Remove(budget);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Transaction/Create
        public IActionResult AddTransaction(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.budgetid = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTransaction([Bind("budgetid, amount_trans, created_at, description")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                
                transaction.updated_at = DateTime.Now;
                _context.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        private bool BudgetExists(int id)
        {
            return _context.Budgetitems.Any(e => e.id == id);
        }
    }
}
