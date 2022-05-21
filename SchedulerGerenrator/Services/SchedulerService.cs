
using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Models.Operations;
using SchedulerGerenrator.Models.Requests;
using SchedulerGerenrator.Models.Response;


namespace SchedulerGerenrator.Services
{
    public class SchedulerService
    {

        /// <summary>
        /// Generate scheduler for light and water for given list of  recipes and tray list
        /// </summary>
        /// <param name="recipes">list of known recipes </param>
        /// <param name="trays">list of tray</param>
        /// <returns>operations scheduler List<TrayResponse> </returns>
        protected List<TrayResponse> TransformToScheduler(List<Recipe> recipes, List<TrayRequest> trays)
        {
            List<TrayResponse> allTraysScheduler = new List<TrayResponse>(trays.Count);

            //TODO:GetRecipes should return dictionary
            var recipeDictionary = recipes.ToDictionary(x => x.Name);

            foreach (var tray in trays)
            {
                recipeDictionary.TryGetValue(tray.RecipeName, out var recipe);
                TrayResponse trayScheduler = null;
                allTraysScheduler.Add(trayScheduler);
            }

            return allTraysScheduler;
        }

        /// <summary>
        /// Generate a scheduler for one given tray and one given recipe
        /// </summary>
        /// <param name="recipe"> a recipe</param>
        /// <param name="tray"> a tray</param>
        /// <returns>scheduler for given tray</returns>
        protected TrayResponse TransformRecipeToScheduler(Recipe recipe, TrayRequest tray)
        {
            TrayResponse trayScheduler = new TrayResponse(tray.TrayNumber);

            if (recipe == null)
            {
                trayScheduler.Exception = "Recipe not found";
                return trayScheduler;
            }



            //IDEA: A recipe could be transformed to a scheduler with relative start time (TimeSpan)
            //in that case it's independent of Tray.StartTime
            //and could be reused for another tray with the same recipeName
            List<LightingTimeSpanBasedOperation> lightOperations = GetLightOperationsFlatList(recipe);// we could calculate count of operations  sum(operations.count*reperitions)
            trayScheduler.Light = GetLightScheduler(lightOperations, tray.StartDate.ToUniversalTime());

            return trayScheduler;
        }

        /// <summary>
        /// Transform complex phase list to a flat lighting operation list. 
        /// </summary>
        /// <param name="recipe">current recipe</param>
        /// <returns>flat operation list</returns>
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
        public List<WateringTimeSpanBasedOperation> GetWaterOperationsFlatList(Recipe recipe)
        {
            var waterOperations = new List<WateringTimeSpanBasedOperation>();
            var operationStart = new TimeSpan(0, 0, 0);
            foreach (WateringPhase phase in recipe.WateringPhases.OrderBy(x => x.Order))
            {
                for (int i = 0; i < phase.Repetitions; i++)
                {
                    var operationEnd = operationStart + new TimeSpan(phase.Hours, phase.Minutes, 0);
                    var waterOperation = new WateringTimeSpanBasedOperation()
                    {

                        Start = operationStart,
                        End = operationEnd,
                        Amount = phase.Amount,
                    };

                    //End of a phase or repetion is a start of next phase/repetition
                    operationStart = operationEnd;
                }
            }

            return waterOperations;
        }

        //Possible optimization:the method could be replaced with model mapping approach
        /// <summary>
        /// Transform a flat list of light operations with timeSpan to list of scheduled records with absolute DateTime
        /// </summary>
        /// <param name="operations">flat list of time related operations </param>
        /// <param name="trayStartDate">start date of growing process</param>
        /// <returns>list of scheduled light oprations with absolute time</returns>
        public List<LightSchedulerRecord> GetLightScheduler(List<LightingTimeSpanBasedOperation> operations, DateTime trayStartDate)
        {
            var scheduledRecords = operations.Select(x => new LightSchedulerRecord()
            {
                StartDate = trayStartDate + x.Start,
                EndDate = trayStartDate + x.End,
                Intencity = x.Intensity
            }).ToList();

            return scheduledRecords;
        }


        /// <summary>
        /// Transform a flat list of water operations with timeSpan to list of scheduled records with absolute DateTime
        /// </summary>
        /// <param name="operations">flat list of time related operations </param>
        /// <param name="trayStartDate">start date of growing process</param>
        /// <returns>list of scheduled water oprations with absolute time</returns>

        public List<WaterSchedulerRecord> GetWaterScheduler(List<WateringTimeSpanBasedOperation> operations, DateTime trayStartDate)
        {
            var scheduledRecords = operations.Select(x => new WaterSchedulerRecord()
            {
                StartDate = trayStartDate + x.Start,
                EndDate = trayStartDate + x.End,
                Amount = x.Amount
            }).ToList();

            return scheduledRecords;
        }
    }
}
