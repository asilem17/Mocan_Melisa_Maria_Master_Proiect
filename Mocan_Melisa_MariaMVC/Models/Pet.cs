using System.ComponentModel.DataAnnotations;

namespace Mocan_Melisa_MariaMVC.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required] 
        public string PetName { get; set; }

        [Required]
        public string PetType { get; set; }

        public int AgeMonths { get; set; }

        public string Size { get; set; }

        public bool Vaccinated { get; set; }

        public string HealthCondition { get; set; }

        public int TimeInShelterDays { get; set; }

        [Required]
        public int BreedId { get; set; }
        public Breed? BreedNavigation { get; set; }

        public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
