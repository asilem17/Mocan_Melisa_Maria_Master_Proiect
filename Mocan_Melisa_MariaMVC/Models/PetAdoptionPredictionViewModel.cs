namespace Mocan_Melisa_MariaMVC.Models
{
    public class PetAdoptionPredictionViewModel
    {
        public int PetId { get; set; }

        // Output
        public string PetName { get; set; } = "";

        public float AdoptionProbability { get; set; }
    }
}
