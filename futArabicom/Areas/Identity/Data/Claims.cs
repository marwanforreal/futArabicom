using futArabicom.Models;
using System.ComponentModel.DataAnnotations;

namespace futArabicom.Areas.Identity.Data
{
    public class Claims
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }

        public User? User { get; set; }

        public Player? Player { get; set; }

        public string? Source { get; set; }

        public DateTime claimDate { get; set; }

        public DateTime timeStamp { get; set; }

        public bool isVerified { get; set; }
    }
}
