using System.ComponentModel.DataAnnotations;

namespace Mocan_Melisa_MariaMVC.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }
        public string DesiredPetType { get; set; }
        public int? MinAgeMonths { get; set; }
        public int? MaxAgeMonths { get; set; }
        public string DesiredSize { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        public int? PetId { get; set; }
        public Pet? Pet { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
