using SchedulerGenerator.Services;
using SchedulerGerenrator.Models.Requests;

namespace NUnitTests.SchedulerServiceTests
{

    public class RecipeService_GetFlatRecipeTests
    {

        private RecipeManipulationService _recipeService;

        [SetUp]
        public void Setup()
        {
            _recipeService = new RecipeManipulationService(null, null);

        }

        [Test]
        public void Test_Recipes_ExpectSameCountOfRecord()
        {


            var recipe = new Recipe("Potato");
            recipe.LightingPhases.Add(new LightingPhase("Phase 1", 0, 24, 0, 5)
            {
                Operations = new List<LightingPhaseOperation>
                {
                    new LightingPhaseOperation(0, 0, LightIntensity.High),
                    new LightingPhaseOperation(20, 0, LightIntensity.Off),
                }
            });

            recipe.WateringPhases.Add( new WateringPhase("Phase 1", 1, 24, 0, 2, 30));

            List<TrayRequest> trays = new List<TrayRequest>()
            {
                new TrayRequest()
                {
                    TrayNumber=1,
                    RecipeName=recipe.Name,
                    StartDate=DateTime.UtcNow
                }
            };

            var result = _recipeService.GetFlatRecipe(recipe);
            Assert.NotNull(result);
            Assert.AreEqual(recipe.Name, result.Name, "Result has wrong Name");

            Assert.AreEqual(2, result.Watering.Count, "Wrong watering operations count");
            Assert.AreEqual(10, result.Lighting.Count, "Wrong lighting operations count");
        }
    }
}
