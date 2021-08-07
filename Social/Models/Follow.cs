using System.ComponentModel.DataAnnotations;

namespace Social.Models
{
    public class Follow
    {
        [Key] public long Id { get; set; }
        public long Follower { get; set; }
        public long Followed { get; set; }
    }
}