using System.ComponentModel.DataAnnotations;

namespace Divvy.Models
{
    public class ExpenseSplit
    {
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        [Required]
        public virtual required Expense Expense { get; set; }

        [Required]
        public virtual required User User { get; set; }
    }
}