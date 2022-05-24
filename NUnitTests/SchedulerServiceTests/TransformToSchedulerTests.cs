

namespace NUnitTests.SchedulerServiceTests
{

    internal class TransformToSchedulerTests
    {
        private SchedulerService _shedulerService;

        [SetUp]
        public void Setup()
        {
            _shedulerService = new SchedulerService(new RecipeManipulationService(null, null));
        }


        [Test]
        public void Test_EmptyRecipes_EmptyTrays_ExpectEmpty()
        {
            List<Recipe> recipes = new List<Recipe>();
            List<TrayRequest> trays = new List<TrayRequest>();

            var result = _shedulerService.TransformToScheduler(recipes, trays);
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }


        [Test]
        public void Test_EmptyRecipes_ExpectRecipeName_NotFound()
        {
            List<Recipe> recipes = new List<Recipe>();
            List<TrayRequest> trays = new List<TrayRequest>()
            {
                new TrayRequest()
                {
                    TrayNumber=1,
                    RecipeName="1",
                    StartDate=DateTime.UtcNow
                }
            };

            var result = _shedulerService.TransformToScheduler(recipes, trays);
            Assert.NotNull(result);
            Assert.AreEqual(trays.Count, result.Count);
            Assert.IsFalse(string.IsNullOrEmpty(result[0].Exception), "This tray record should contain an exception message");
        }

        [Test]
        public void Test3_Recipes_ExpectSameCountOfRecord()
        {
            List<Recipe> recipes = new List<Recipe>();

            var recipe3 = new Recipe("Potato");
            recipe3.LightingPhases.Add(new LightingPhase("Phase 3", 0, 24, 0, 5)
            {
                Operations = new List<LightingPhaseOperation>
            {
                new LightingPhaseOperation(0, 0, LightIntensity.High),
                new LightingPhaseOperation(20, 0, LightIntensity.Off),
            }
            });

            recipe3.WateringPhases.Add(
            new WateringPhase("Phase 3", 3, 24, 0, 2, 30));

            recipes.Add(recipe3);

            List<TrayRequest> trays = new List<TrayRequest>()
            {
                new TrayRequest()
                {
                    TrayNumber=1,
                    RecipeName=recipe3.Name,
                    StartDate=DateTime.UtcNow
                }
            };

            var result = _shedulerService.TransformToScheduler(recipes, trays);
            Assert.NotNull(result);
            Assert.AreEqual(trays.Count, result.Count);


            Assert.AreEqual(2, result[0].Watering.Count, "Wrong watering operations count");
            Assert.AreEqual(10, result[0].Lighting.Count, "Wrong lighting operations count");
        }
    }
}
