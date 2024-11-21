using System.ComponentModel.DataAnnotations;

namespace Divvy.Models
{
    public class GroupMember
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public virtual required Group Group { get; set; }

        [Required]
        public virtual required User User { get; set; }
    }
}