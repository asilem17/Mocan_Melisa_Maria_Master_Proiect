using System.ComponentModel.DataAnnotations;

namespace Mocan_Melisa_MariaMVC.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        public int? PetId { get; set; }
        public Pet? Pet { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; }

        public DateTime Date { get; set; }
    }
}
