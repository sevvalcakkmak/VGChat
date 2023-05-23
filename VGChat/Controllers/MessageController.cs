using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VGChat.Areas.Identity.Data;
using VGChat.Data;
using VGChat.Models;

namespace VGChat.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _db; //to work with db
        private readonly ILogger<MessageController> _logger;
        private readonly UserManager<VGChatUser> _userManager;

        public MessageController(ApplicationDbContext db, ILogger<MessageController> logger, UserManager<VGChatUser> userManager)
        {
            _db = db;
            _logger = logger;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["UserName"] = _userManager.GetUserName(this.User);
            IEnumerable<Message> objMessageList = _db.Messages.ToList(); //creating a list from db
            return View(objMessageList); //passing that list
        }
    }
}
