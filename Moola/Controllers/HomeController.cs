using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Moola.Logic;
using Moola.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Moola.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;
        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        //title page
        public IActionResult Index()
        {
            return View();
        }

        //privacy policy
        public IActionResult Privacy()
        {
            return View();
        }

        //information about the app
        public IActionResult Information()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}