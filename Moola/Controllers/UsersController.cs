using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Moola.Models;
using System.Security.Claims;

namespace Moola.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyContext _context;
        public UsersController(MyContext context) => _context = context;

        //render the login page
        public IActionResult Login()
        {
            return View();
        }

        //login the user
        [HttpPost]
        public async Task<IActionResult> Login(Account account)
        {
            var hashedPassword = PasswordEncryption.HashPassword(account.Password);
            var dbAccount = _context.Accounts.FirstOrDefault(a => a.Login == account.Login && a.Password == hashedPassword);
            if (dbAccount == null)
            {
                ModelState.AddModelError("Login", "Invalid login or password");
                return RedirectToAction("Login");
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbAccount.Login),
                new Claim(ClaimTypes.Role, "User")
            }, CookieAuthenticationDefaults.AuthenticationScheme)));

            return RedirectToAction("Balance");
        }

        //logout the user
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        //hash the password
        public IActionResult HashAll()
        {
            var accounts = _context.Accounts.ToList();
            accounts.ForEach(a => a.Password = PasswordEncryption.HashPassword(a.Password));
            _context.SaveChanges();
            return RedirectToAction("Balance");
        }

        //display balance
        [Authorize]
        public IActionResult Balance()
        {
            decimal totalAmount = _context.Incomes.Sum(i => i.Amount) - _context.Expenses.Sum(e => e.Amount);
            decimal totalIncomesAmount = _context.Incomes.Sum(i => i.Amount);
            decimal totalExpensesAmount = _context.Expenses.Sum(e => e.Amount);
            decimal cardBalance = _context.Incomes.Include(i => i.Finance).Where(i => i.Finance.Name == "Credit").Sum(i => i.Amount) - _context.Expenses.Include(e => e.Finance).Where(e => e.Finance.Name == "Credit").Sum(e => e.Amount);
            decimal cashBalance = _context.Incomes.Include(i => i.Finance).Where(i => i.Finance.Name == "Cash").Sum(i => i.Amount) - _context.Expenses.Include(e => e.Finance).Where(e => e.Finance.Name == "Cash").Sum(e => e.Amount);
            ViewBag.TotalAmount = totalAmount;
            ViewBag.TotalIncomesAmount = totalIncomesAmount;
            ViewBag.TotalExpensesAmount = totalExpensesAmount;
            ViewBag.CardBalance = cardBalance;
            ViewBag.CashBalance = cashBalance;
            return View();
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            var newId = 1;
            if (_context.Users.Any()) newId = _context.Users.Max(u => u.Id) + 1;
            _context.Users.Add(user with { Id = newId });
            _context.SaveChanges();
            return RedirectToAction("Profile", new {id=newId});
        }

        //GET: Users/Profile
        public IActionResult Profile(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }



        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Email,Birthday,Id")] User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

    }
}
