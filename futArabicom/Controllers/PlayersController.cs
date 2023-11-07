using futArabicom.Data;
using futArabicom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Drawing;
using System.Net.NetworkInformation;
using futArabicom.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace futArabicom.Controllers
{
    [Route("pages")]
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context; 
        }

        [Route("index")]
        public IActionResult Index()
        {
            var data = _context.Players.ToList();

            List<string> playerImagesUrls = new();

            foreach (Player player in data)
            {
                playerImagesUrls.Add(ExtractImageUrl(player));
            }

            ViewBag.imageUrls = playerImagesUrls;

            return View("Index", data);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [Route("create")]
        [HttpPost]
        public IActionResult Create(Player player, IFormFile playerImage)
        {
            player.Pace = 86;
            player.Defending = 75;
            player.Shooting = 60;
            player.Passing = 69;
            player.Physical = 80;
            player.Dribbling = 59;

            if(playerImage != null)
            {
                var imageData = TransformImageData(playerImage);
                player.Image = imageData;
            }
            else
            {
                player.Image = new byte[0];
            }

            _context.Players.Add(player);
            _context.SaveChanges(); 
            return View();
        }

        private byte[] TransformImageData(IFormFile playerImage)
        {
            MemoryStream ms = new MemoryStream();
            playerImage.CopyTo(ms);
            var imageData = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return imageData;
        }

        [HttpGet]
        [Route("details")]
        public IActionResult Details(PlayerDetailsViewModel pageModel)
        {
            var currentPlayer = _context.Players.SingleOrDefault(p => p.Id == pageModel.pageId);

            PlayerDetailsViewModel viewModel = new PlayerDetailsViewModel()
            {
                Player = currentPlayer,
                Comments = _context.Comments.Where(p => p.Player == currentPlayer).Include(user => user.User).ToList(),
                Claims = _context.Claims.Where(p => p.Player == currentPlayer).Include(user => user.User).ToList()
            };

            //Player player = _context.Players.SingleOrDefault(p => p.Id == id);

            if(viewModel.Player.Image != null)
            {
                var imageDataUrl = ExtractImageUrl(viewModel.Player);

                ViewBag.imageUrl = imageDataUrl;
            }

            Random random = new Random();
            //List<int> random12 = numbers.OrderBy(x => random.Next()).Take(12).ToList();

            var allPlayers = _context.Players.ToList();

            var similarPlayers = allPlayers.OrderBy(p => random.Next()).Take(6).ToList();

            viewModel.SimilarPlayers = similarPlayers;

            List<string> similarPlayerImagesUrls = new();

            foreach (Player player in similarPlayers)
            {
                similarPlayerImagesUrls.Add(ExtractImageUrl(player));
            }

            ViewBag.imageUrls = similarPlayerImagesUrls;

            return View("Details", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> addComment(PlayerDetailsViewModel pageModel)
        {
            var currentUserName = User.Identity.Name;

            var currentUserObject = _context.Users.FirstOrDefault(x => x.UserName == currentUserName);

            var currentPlayerObject = _context.Players.FirstOrDefault(x => x.Id == pageModel.pageId);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return View();
            }

            try
            {
                _context.Comments.Add(new Comment
                {
                    Content = pageModel.NewComment.Content,
                    User = currentUserObject,
                    Player = currentPlayerObject
                });
            }
            catch (Exception)
            {
                throw;
            }

            await _context.SaveChangesAsync();

            var fillComments = _context.Comments.Where(p => p.Player == currentPlayerObject).Include(user => user.User).ToList(); 

            PlayerDetailsViewModel viewModel = new PlayerDetailsViewModel()
            {
                Player = _context.Players.SingleOrDefault(p => p.Id == pageModel.pageId),
                Comments = fillComments,
                pageId = currentPlayerObject.Id
            };

            return RedirectToAction("Details", viewModel);
        }

        private string ExtractImageUrl(Player player)
        {
            var image = player.Image;
            string imageBase64 = Convert.ToBase64String(image);

            var url = string.Format("data:image/png; base64,{0}"
            , imageBase64);

            return url;
        }

        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(int? id)
        {
            var player = _context.Players.Include(p => p.Comments).Include(p => p.Claims).SingleOrDefault(p => p.Id == id); 

            if(player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(PlayerEditModel player, IFormFile? playerImage = null)
        {
            Player currPlayer = _context.Players.Include(p => p.Comments).Include(p => p.Claims).FirstOrDefault(p => p.Id == player.Id);

            if (player == null)
            {
                // Handle the case when the player doesn't exist
                return NotFound(); // Or any other appropriate response
            }

            currPlayer.Name = player.Name;
            currPlayer.NameAr = player.NameAr;
            currPlayer.Country = player.Country;
            currPlayer.Description = player.Description;

            if(playerImage != null)
            {
                var imageData = TransformImageData(playerImage);
                currPlayer.Image = imageData;
            }

            ModelState.Clear();
            var x = TryValidateModel(currPlayer);



            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return View();
            }

            _context.SaveChanges();

            return View();
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var player = _context.Players.SingleOrDefault(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            return View(new PlayerEditModel { Name = player.Name, NameAr = player.NameAr, Country = player.Country, Description = player.Description, Id = player.Id });
        }
    }
}
