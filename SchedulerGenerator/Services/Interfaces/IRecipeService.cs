using SchedulerGerenrator.Models.ExternalApi.IGS;

namespace SchedulerGerenrator.Services.Interfaces
{
    
    public interface IRecipeService
    {
        Task<List<Recipe>> GetRecipesAsync();
    }
}
