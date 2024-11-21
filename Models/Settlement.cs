using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Divvy.Models
{
    public class Settlement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettlementId { get; set; }

        public int GroupId { get; set; }
        public int PayerId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public virtual required Group Group { get; set; }

        [Required]
        public virtual required User Payer { get; set; }

        [Required]
        public virtual required User Receiver { get; set; }
    }
}