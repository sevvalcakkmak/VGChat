using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VGChat.Areas.Identity.Data;
using VGChat.Data;
using VGChat.Models;

namespace VGChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly VGChatAuthDbContext _db;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<VGChatUser> _userManager;

        public HomeController(VGChatAuthDbContext db, ILogger<HomeController> logger, UserManager<VGChatUser> userManager)
        {
            _db = db;
            _logger = logger;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<VGChatUser> objUserList = _db.Users.ToList();
            ViewData["UserID"] = _userManager.GetUserId(this.User);

            VGChatUser user = await _userManager.GetUserAsync(this.User);
            String firstName = user.FirstName;

            ViewData["UserName"] = firstName;
            return View(objUserList);
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