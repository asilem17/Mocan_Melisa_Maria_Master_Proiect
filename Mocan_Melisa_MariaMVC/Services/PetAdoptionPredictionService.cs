using Microsoft.EntityFrameworkCore;
using Mocan_Melisa_MariaMVC.Data;
using Mocan_Melisa_MariaMVC.Models;

namespace Mocan_Melisa_MariaMVC.Services
{
    public class PetAdoptionService : IPetAdoptionService
    {
        private readonly HttpClient _httpClient;
        private readonly PetsAdoptionContext _context;

        public PetAdoptionService(HttpClient httpClient, PetsAdoptionContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<float> PredictAdoptionProbabilityAsync(int petId)
        {
            var pet = await _context.Pet
                .Include(p => p.BreedNavigation)
                .FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null) return 0f;
            int healthNumeric = pet.HealthCondition switch
            {
                "Medical condition" => 1,
                "Healthy" => 0,
                _ => 0 
            };


            var mlInput = new
            {
                petType = pet.PetType,
                breed = pet.BreedNavigation?.BreedName ?? "",
                ageMonths = (float)pet.AgeMonths,
                size = pet.Size,
                vaccinated = pet.Vaccinated ? 1 : 0,
                healthCondition = healthNumeric,
                timeInShelterDays = (float)pet.TimeInShelterDays,
                adoptionLikelihood = 0
            };


            var response = await _httpClient.PostAsJsonAsync("/predict", mlInput);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PetsAdoptionPrediction.ModelOutput>();
            return result?.Score ?? 0f;
        }
    }
}
