using futArabicom.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace futArabicom.Controllers
{
    public class ClaimsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int playerId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Claims claim)
        {
            return RedirectToAction("Details", new { id = claim.Player.Id });
        }
    }
}
