using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Divvy.Models
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }

        public required string GroupName { get; set; }
        public required int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public virtual required User CreatedBy { get; set; }

        public virtual ICollection<GroupMember> GroupMembers { get; set; } = [];
        public virtual ICollection<Expense> Expenses { get; set; } = [];
        public virtual ICollection<Settlement> Settlements { get; set; } = [];
    }
}