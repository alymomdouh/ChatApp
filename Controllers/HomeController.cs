using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<AppUser> userManager;

        public HomeController(ApplicationDbContext _applicationDbContext, UserManager<AppUser> _userManager)
        {
            applicationDbContext = _applicationDbContext;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.currentUser = currentUser;
            var messages = await applicationDbContext.Messages.ToListAsync();
            return View(messages);
        }
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                await applicationDbContext.Messages.AddAsync(message);
                await applicationDbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        public IActionResult Privacy()
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
