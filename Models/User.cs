using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divvy.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Group> GroupsCreated { get; set; } = [];
        public virtual ICollection<GroupMember> GroupMembers { get; set; } = [];
        public virtual ICollection<Expense> ExpensesPaid { get; set; } = [];
        public virtual ICollection<Settlement> SettlementsMade { get; set; } = [];
        public virtual ICollection<Settlement> SettlementsReceived { get; set; } = [];
        public virtual ICollection<ExpenseSplit> ExpenseSplits { get; set; } = [];
    }
}