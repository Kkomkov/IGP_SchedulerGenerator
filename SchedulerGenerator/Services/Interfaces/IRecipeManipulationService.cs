using SchedulerGenerator.Models.Operations;
using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Models.Operations;

namespace SchedulerGenerator.Services.Interfaces
{
    public interface IRecipeManipulationService
    {
        
        FlatRecipe GetFlatRecipe(Recipe recipe);
      
    }
}