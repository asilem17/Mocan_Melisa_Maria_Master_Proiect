using System.ComponentModel.DataAnnotations;

namespace Mocan_Melisa_MariaMVC.Models
{
    public class Breed
    {
        public int Id { get; set; }

        [Required]
        public string BreedName { get; set; }

        [Required]
        public string AnimalType { get; set; }

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
