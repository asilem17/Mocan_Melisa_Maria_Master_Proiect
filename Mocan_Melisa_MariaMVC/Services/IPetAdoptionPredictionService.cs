using Mocan_Melisa_MariaMVC.Models;

namespace Mocan_Melisa_MariaMVC.Services
{
    public interface IPetAdoptionService
    {
        Task<float> PredictAdoptionProbabilityAsync(int petId);
    }
}
