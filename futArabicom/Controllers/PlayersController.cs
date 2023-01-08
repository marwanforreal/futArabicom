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
            var data = _context.Players.ToList();

            return View("Index", data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            player.Pace = 86;
            player.Defending = 75;
            player.Shooting = 60;
            player.Passing = 69;
            player.Physical = 80;
            player.Dribbling = 59;
            _context.Players.Add(player);
            _context.SaveChanges(); 
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Player player = _context.Players.SingleOrDefault(p => p.Id == id);

            return View("Details", player);
        }
    }
}
