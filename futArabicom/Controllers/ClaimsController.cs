using futArabicom.Areas.Identity.Data;
using futArabicom.Data;
using futArabicom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace futArabicom.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int playerId)
        {
            TempData["playerId"] = playerId; 

            return View();
        }

        [HttpPost]
        public IActionResult Create(Claims claim)
        {
            var playerIdString = TempData["playerId"].ToString();

            int playerId = 0; 

            int.TryParse(playerIdString, out playerId);

            var playerModel = getPlayerById(playerId);

            claim.Player = playerModel;

            claim.isVerified = false; 

            claim.timeStamp = DateTime.UtcNow;

            claim.User = getUserByUsername(User.Identity.Name);

            return RedirectToAction("Details", "Players", new { id = claim.Player.Id });
        }

        private Player getPlayerById(int playerId)
        {
            return _context.Players.SingleOrDefault(p => p.Id == playerId);
        }

        private User getUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username);
        }
    }
}
