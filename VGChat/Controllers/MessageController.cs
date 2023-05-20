using Microsoft.AspNetCore.Mvc;

namespace VGChat_demo.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
   