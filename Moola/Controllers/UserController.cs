using Moola.Logic;
using Moola.Models;

namespace Moola.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext _context;
        public UserController(MyContext context) => _context = context;

       
    }
}
