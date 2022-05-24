using SchedulerGenerator.Models.Operations;
using SchedulerGenerator.Models.ExternalApi.IGS;
using SchedulerGenerator.Models.Operations;

namespace SchedulerGenerator.Services.Interfaces
{
    public interface IRecipeManipulationService
    {
        
        FlatRecipe GetFlatRecipe(Recipe recipe);
      
    }
}