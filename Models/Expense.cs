using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Divvy.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseId { get; set; }

        public int GroupId { get; set; }
        public int PaidById { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public virtual required Group Group { get; set; }

        [Required]
        public virtual required User PaidBy { get; set; }

        public virtual ICollection<ExpenseSplit> ExpenseSplits { get; set; } = [];
    }
}