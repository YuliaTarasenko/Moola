using Moola.Logic;
using Moola.Models;

namespace Moola.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext _context;
        public UserController(MyContext context) => _context = context;

        public IActionResult Balance()
        {
            decimal totalAmount = _context.Incomes.Sum(i => i.Amount) - _context.Expenses.Sum(e => e.Amount);
            decimal totalIncomesAmount = _context.Incomes.Sum(i => i.Amount);
            decimal totalExpensesAmount = _context.Expenses.Sum(e => e.Amount);
            ViewBag.TotalAmount = totalAmount;
            ViewBag.TotalIncomesAmount = totalIncomesAmount;
            ViewBag.TotalExpensesAmount = totalExpensesAmount;
            return View();
        }
    }
}
