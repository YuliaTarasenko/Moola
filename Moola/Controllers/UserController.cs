using Microsoft.AspNetCore.Mvc;

using Moola.Models;

namespace Moola.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext _context;
        public UserController(MyContext context) => _context = context;

        public IActionResult Balance()
        {
            return View();
        }

        public IActionResult Incomes()
        {
            return View();
        }

        public IActionResult AddIncome()
        {
            return View();
        }

    [HttpPost]
        public IActionResult AddIncome(Income income)
        {
            var newId = _context.Incomes.Count() + 1;
            _context.Incomes.Add(income with { Id = newId});
            _context.SaveChanges();
            return RedirectToAction("Incomes");
        }

        public IActionResult EditIncome(int id)
        {
            return View(_context.Incomes.Find(id));
        }

        [HttpPost]
        public IActionResult EditIncome(Income income)
        {
            var dbIncome = _context.Incomes.Find(income.Id);
            _context.Entry(dbIncome).CurrentValues.SetValues(income);
            _context.SaveChanges();
            return RedirectToAction("Incomes");
        }

        
    }
}
