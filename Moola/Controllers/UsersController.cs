using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
            var dbAccount = _context.Accounts.FirstOrDefault(a => a.Login == account.Login && a.Password==hashedPassword);
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

        //public UsersController(MyContext context)=> _context = context;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;

        //public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        ////[HttpPost]
        ////public async Task<IActionResult> Register(RegisterViewModel model)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        ////        var result = await _userManager.CreateAsync(user, model.Password);

        ////        if (result.Succeeded)
        ////        {
        ////            await _signInManager.SignInAsync(user, isPersistent: false);
        ////            return RedirectToAction("Index", "Home");
        ////        }

        ////        foreach (var error in result.Errors)
        ////        {
        ////            ModelState.AddModelError("", error.Description);
        ////        }
        ////    }

        ////    return View(model);
        ////}

        //[HttpGet]
        //public IActionResult RegisterConfirmation()
        //{
        //    return View();
        //}






        //// GET: Users
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Users != null ? 
        //                  View(await _context.Users.ToListAsync()) :
        //                  Problem("Entity set 'MyContext.Users'  is null.");
        //}

        //// GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Birthday,Id")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

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

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return Problem("Entity set 'MyContext.Users'  is null.");
        //    }
        //    var user = await _context.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //  return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
