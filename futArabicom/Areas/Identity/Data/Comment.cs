using System.ComponentModel.DataAnnotations;

namespace futArabicom.Areas.Identity.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
