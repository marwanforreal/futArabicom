using futArabicom.Data;
using futArabicom.Models;
using Microsoft.AspNetCore.Mvc;

namespace futArabicom.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            //var data = _context.Players.ToList();

            List<Player> players = new List<Player>();

            players.Add(new Player(1, "Marwan", "Real Madrid"));

            players.Add(new Player(2, "Marvin", "Real Madrid"));

            players.Add(new Player(3, "Marian", "Real Madrid"));

            return View("Index", players);
        }
    }
}
