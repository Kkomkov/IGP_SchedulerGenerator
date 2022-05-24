using Microsoft.Extensions.Caching.Memory;
using SchedulerGenerator.Models.Operations;
using SchedulerGenerator.Services.Interfaces;
using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Models.Operations;

namespace SchedulerGenerator.Services
{
    public class RecipeManipulationService : IRecipeManipulationService
    {
        private readonly ILogger<RecipeManipulationService> logger;
        private readonly IMemoryCache memoryCache;

        public RecipeManipulationService(ILogger<RecipeManipulationService> logger, IMemoryCache memoryCache)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        private const string _keyPrefix = "FlatRecipe_Name:";
        protected string GetKey(string recipeName)
        {
            return _keyPrefix + recipeName;
        }
        /// <summary>
        /// Try get a flat recipe from cache 
        /// if not found call ConvertRecipeToFlatRecipe
        /// </summary>
        /// <param name="recipe">source recipe</param>
        /// <returns>flat recipe</returns>
        public FlatRecipe GetFlatRecipe(Recipe recipe)
        {
            FlatRecipe result = null;

            if (memoryCache == null)
            {
                result = ConvertRecipeToFlatRecipe(recipe);
            }
            else
            {
                string key = GetKey(recipe.Name);

                result = memoryCache.GetOrCreate(key, (cacheEntry) => //try read from cache if no then execute a method to conver
                        {
                            var value = ConvertRecipeToFlatRecipe(recipe);

                            cacheEntry.AbsoluteExpirationRelativeToNow = new TimeSpan(0, 0, 30);
                            cacheEntry.SetValue(value);

                            return value;
                        });
            }


            return result;
        }

        /// <summary>
        /// Convert recipe to set of list  flat ligting operaions and list of flat watering operations
        /// </summary>
        /// <param name="recipe">source recipe</param>
        /// <returns>FlatRecipe</returns>
        public FlatRecipe ConvertRecipeToFlatRecipe(Recipe recipe)
        {
            var flatRecipe = new FlatRecipe(recipe.Name);
            flatRecipe.Lighting = GetLightOperationsFlatList(recipe);
            flatRecipe.Watering = GetWaterOperationsFlatList(recipe);

            return flatRecipe;
        }

        /// <summary>
        /// Transform Recipe.Ligting to flat ligting operaitons
        /// </summary>
        /// <param name="recipe">source recipe</param>
        /// <returns>list of flat ligting operations</returns>
        public List<LightingTimeSpanBasedOperation> GetLightOperationsFlatList(Recipe recipe)
        {
            var lightOperations = new List<LightingTimeSpanBasedOperation>();
            //LightingPhase previousPhase = null;
            TimeSpan phaseStart = new TimeSpan(0, 0, 0);
            LightingTimeSpanBasedOperation previousOperation = null;

            foreach (LightingPhase phase in recipe.LightingPhases.OrderBy(x => x.Order))
            {
                for (int i = 0; i < phase.Repetitions; i++)
                {
                    var phaseEnd = phaseStart + new TimeSpan(phase.Hours, phase.Minutes, 0);

                    if (phase.Operations != null)
                    {
                        foreach (var operation in phase.Operations)
                        {
                            var currentOperation = new LightingTimeSpanBasedOperation()
                            {
                                Start = phaseStart + new TimeSpan(operation.OffsetHours, operation.OffsetMinutes, 0),
                                Intensity = operation.LightIntensity
                            };

                            lightOperations.Add(currentOperation);

                            if (previousOperation != null)
                            {
                                previousOperation.End = currentOperation.Start;
                            }

                            previousOperation = currentOperation;
                        }
                        //End of the last operation is end of the phase
                        if (previousOperation != null)// check for a case phase has no operations
                        {
                            previousOperation.End = phaseEnd;
                        }
                    }

                    //the end of current phase is a start of next phase;
                    phaseStart = phaseEnd;
                    previousOperation = null;
                }
            }
            return lightOperations;
        }
        /// <summary>
        /// Transform Recipe.Watering to flat watering operations
        /// </summary>
        /// <param name="recipe">source recipe</param>
        /// <returns>list of flat watering operations</returns>
        public List<WateringTimeSpanBasedOperation> GetWaterOperationsFlatList(Recipe recipe)
        {
            var waterOperations = new List<WateringTimeSpanBasedOperation>();
            var operationStart = new TimeSpan(0, 0, 0);
            foreach (WateringPhase phase in recipe.WateringPhases.OrderBy(x => x.Order))
            {
                for (int i = 0; i < phase.Repetitions; i++)
                {
                    var operationEnd = operationStart + new TimeSpan(phase.Hours, phase.Minutes, 0);
                    waterOperations.Add(new WateringTimeSpanBasedOperation()
                    {
                        Start = operationStart,
                        End = operationEnd,
                        Amount = phase.Amount,
                    });

                    //End of a phase or repetion is a start of next phase/repetition
                    operationStart = operationEnd;
                }
            }

            return waterOperations;
        }
    }
}
