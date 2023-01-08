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
            player.Pace = 99;
            player.Defending = 99;
            player.Shooting = 99;
            player.Passing = 99;
            player.Physical = 99;
            player.Dribbling = 99;
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
