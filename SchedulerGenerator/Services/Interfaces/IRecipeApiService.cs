using SchedulerGerenrator.Models.ExternalApi.IGS;

namespace SchedulerGerenrator.Services.Interfaces
{
    
    public interface IRecipeApiService
    {
        Task<List<Recipe>> GetRecipesAsync();
    }
}
