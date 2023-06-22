namespace Moola.Controllers
{
    public class IncomesController : Controller
    {
        private readonly MyContext _context;

        public IncomesController(MyContext context) => _context = context;

        // GET: Incomes
        public IActionResult Incomes() => View();

        //GET: All Incomes
        public IActionResult SeeMore() => View();

        // GET: Incomes/Create
        public IActionResult Create() => View();

        //POST: Incomes/Create
        [HttpPost]
        public IActionResult Create(Income income)
        {
            var newId = 1;
            if (_context.Incomes.Any()) newId = _context.Incomes.Max(i => i.Id) + 1;
            _context.Incomes.Add(income with { Id = newId });
            _context.SaveChanges();
            return RedirectToAction("Incomes");
        }
      
        // GET: Incomes/Edit
        public IActionResult Edit(int id) => View(_context.Incomes.Find(id));

        //POST: Incomes/Edit
        [HttpPost]
        public IActionResult Edit(Income income)
        {
            var dbIncome = _context.Incomes.Find(income.Id);
            _context.Entry(dbIncome).CurrentValues.SetValues(income);
            _context.SaveChanges();
            return RedirectToAction("Incomes");
        }

        //GET: Incomes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Incomes == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes
                .Include(i => i.Finance)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        //GET: Incomes/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Incomes == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes
                .Include(i => i.Finance)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // POST: Incomes/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Incomes == null)
            {
                return Problem("Entity set 'MyContext.Incomes'  is null.");
            }
            var income = await _context.Incomes.FindAsync(id);
            if (income != null)
            {
                _context.Incomes.Remove(income);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Incomes");
        }
    }
}
