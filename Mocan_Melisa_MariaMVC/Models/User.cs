using System.ComponentModel.DataAnnotations;

namespace Mocan_Melisa_MariaMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
