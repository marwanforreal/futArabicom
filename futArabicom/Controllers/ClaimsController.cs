using futArabicom.Areas.Identity.Data;
using futArabicom.Data;
using futArabicom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public async Task<IActionResult> Create(Claims claim)
        {
            var playerIdString = TempData["playerId"].ToString();

            int playerId = 0; 

            int.TryParse(playerIdString, out playerId);

            var playerModel = getPlayerById(playerId);

            claim.Player = playerModel;

            claim.isVerified = false; 

            claim.timeStamp = DateTime.UtcNow;

            claim.User = getUserByUsername(User.Identity.Name);

            if (claim.Player != null)
            {
                // Update the lastUpdate field for the player
                claim.Player.lastUpdate = DateTime.Now; // You can set the appropriate date and time
            }

            try
            {
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
             

            return RedirectToAction("Details", "Players", new PlayerDetailsViewModel { PlayerId = claim.Player.Id});
        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    var player = _context.Players.SingleOrDefault(p => p.Id == id);

        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(player);
        //}

        [HttpDelete]
        public IActionResult Delete(Claims claim)
        {
            var fullClaim = _context.Claims.Where(p=> p.Id == claim.Id).Include(p => p.Player).FirstOrDefault();

            if (claim == null)
            {
                // Handle the case where the claim doesn't exist.
                return NotFound();
            }

            if (fullClaim.Player != null)
            {
                // Update the lastUpdate field for the player
                fullClaim.Player.lastUpdate = DateTime.Now; // You can set the appropriate date and time
            }

            // Remove the claim
            _context.Claims.Remove(fullClaim);
            _context.SaveChanges();

            //_context.Claims.Remove(claim);
            //_context.SaveChanges();
            return View("Index");
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
