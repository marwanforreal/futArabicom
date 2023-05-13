namespace futArabicom.Areas.Identity.Data
{
    public class Comment
    {
        public string Content { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
