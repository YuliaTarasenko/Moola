using Moola.Logic;
using Moola.Models;

namespace Moola.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly MyContext _context;

        public ExpensesController(MyContext context) => _context = context;

        // GET: Expenses
        public IActionResult Expenses() => View();

        // GET: All Expenses
        public IActionResult SeeMore() => View();

        // GET: Expenses/Create
        public IActionResult Create() => View();

        // POST: Expenses/Create
        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            var newId = 1;
            if (_context.Expenses.Any()) newId = _context.Expenses.Max(e => e.Id) + 1;
            _context.Expenses.Add(expense with { Id = newId });
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        // GET: Expenses/Edit
        public IActionResult Edit(int id) => View(_context.Expenses.Find(id));

        // POST: Expenses/Edit
        [HttpPost]
        public IActionResult Edit(Expense expense)
        {
            var dbExpense = _context.Expenses.Find(expense.Id);
            _context.Entry(dbExpense).CurrentValues.SetValues(expense);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        //GET: Expenses/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.Finance)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.Finance)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expenses == null)
            {
                return Problem("Entity set 'MyContext.Expenses'  is null.");
            }
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Expenses");
        }
    }
}


