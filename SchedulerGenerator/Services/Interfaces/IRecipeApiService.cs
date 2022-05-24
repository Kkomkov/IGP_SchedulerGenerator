using SchedulerGenerator.Models.ExternalApi.IGS;

namespace SchedulerGenerator.Services.Interfaces
{
    
    public interface IRecipeApiService
    {
        Task<List<Recipe>> GetRecipesAsync();
    }
}
