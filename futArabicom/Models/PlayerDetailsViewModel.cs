using futArabicom.Areas.Identity.Data;

namespace futArabicom.Models
{
    public class PlayerDetailsViewModel
    {
        public Player? Player { get; set; }

        public IEnumerable<Comment>? Comments { get; set; }

        public IEnumerable<Claims>? Claims { get; set; }

        public Comment? NewComment { get; set; }

        public int? pageId { get; set; }

        public List<Player> SimilarPlayers { get; set; }
    }
}
