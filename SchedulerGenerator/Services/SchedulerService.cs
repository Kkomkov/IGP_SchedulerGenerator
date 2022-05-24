
using SchedulerGenerator.Models.Operations;
using SchedulerGenerator.Services.Extensions;
using SchedulerGenerator.Services.Interfaces;
using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Models.Operations;
using SchedulerGerenrator.Models.Requests;
using SchedulerGerenrator.Models.Response;
using SchedulerGerenrator.Services.Interfaces;

namespace SchedulerGerenrator.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IRecipeManipulationService recipeManipulationService;

        public SchedulerService(IRecipeManipulationService recipeManipulationService)
        {
            this.recipeManipulationService = recipeManipulationService;
        }
      
        public List<TrayResponse> TransformToScheduler(List<Recipe> recipes, List<TrayRequest> trays)
        {
            List<TrayResponse> allTraysScheduler = new List<TrayResponse>(trays.Count);

            TrayResponse trayScheduler = null;
            var recipeDictionary = recipes.ToDictionary(x => x.Name);
            //try get schedule for each tray
            foreach (var tray in trays)
            {
                recipeDictionary.TryGetValue(tray.RecipeName, out var recipe);

                if (recipe != null)
                {   
                    // get FlatRecipe
                    // order of operations for given recipename with relative (TimeStam) start and end operation time
                    FlatRecipe flatRecipe = recipeManipulationService.GetFlatRecipe(recipe);

                    // init Flat recipe with tray start date
                    trayScheduler = flatRecipe.ToSchedulerRecord(tray.TrayNumber, tray.StartDate);
                }
                else
                {
                    trayScheduler = new TrayResponse(tray.TrayNumber)
                    {
                        Exception = $"There is no recipe with name '{tray.RecipeName}'"
                    };
                }

                allTraysScheduler.Add(trayScheduler);
            }

            return allTraysScheduler;
        }

        
       

    }
}
