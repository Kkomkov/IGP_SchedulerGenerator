using SchedulerGenerator.Services;

namespace NUnitTests.SchedulerServiceTests
{
    public class RecipeService_GetWaterOperationsFlatListTests
    {
        //private SchedulerService _shedulerService;
        private RecipeManipulationService _recipeService;
        [SetUp]
        public void Setup()
        {
            _recipeService = new RecipeManipulationService(null, null);
           // _shedulerService = new SchedulerService(new RecipeManipulationService(null, null));
        }

        private static IEnumerable<TestCaseData> TestDataGetOperationsFlatList =>
           new List<TestCaseData>
           {
                new TestCaseData( 0,new Recipe("3") { }),
                new TestCaseData( 1, // expected watering operations
                                  new Recipe("2") {
                                        WateringPhases = new List<WateringPhase>() {
                                                new WateringPhase("wp1",0,1,0,1,10)
                                        }
                                    }

                ),
                new TestCaseData (3,//expected water operations count
                    new Recipe("1")  {

                        WateringPhases = new List<WateringPhase>() {
                            new WateringPhase("wp1",0,1,0,1,10),
                            new WateringPhase("wp2",0,2,0,2,10),
                        }
                    }
                )
           };


        [Test]
        public void TestHardcodedRecipe_1()
        {
            var recipe = new Recipe("1")
            {

                WateringPhases = new List<WateringPhase>() {
                            new WateringPhase("wp1",0,1,0,1,10),
                            new WateringPhase("wp2",0,2,0,2,20),
                        }
            };


            var flatOperationList = _recipeService.GetWaterOperationsFlatList(recipe);
            Assert.NotNull(flatOperationList, "Flat operations list should not be null");
            flatOperationList.OrderBy(x => x.Start);


            Assert.AreEqual(flatOperationList.Count, 3, "Wrong count of records");
            Assert.AreEqual(flatOperationList[0].Start, new TimeSpan(0, 0, 0)); Assert.AreEqual(flatOperationList[0].End, new TimeSpan(1, 0, 0)); Assert.AreEqual(flatOperationList[0].Amount, 10);
            Assert.AreEqual(flatOperationList[1].Start, new TimeSpan(1, 0, 0)); Assert.AreEqual(flatOperationList[1].End, new TimeSpan(3, 0, 0)); Assert.AreEqual(flatOperationList[1].Amount, 20);
            Assert.AreEqual(flatOperationList[2].Start, new TimeSpan(3, 0, 0)); Assert.AreEqual(flatOperationList[2].End, new TimeSpan(5, 0, 0)); Assert.AreEqual(flatOperationList[2].Amount, 20);

        }



        [Test, TestCaseSource(nameof(TestDataGetOperationsFlatList))]
        public void Test2(int expectedOperationCount, Recipe recipe)
        {

            var flatOperationList = _recipeService.GetWaterOperationsFlatList(recipe);

            Assert.NotNull(flatOperationList, "Flat operations list should not be null");
            Assert.AreEqual(expectedOperationCount, flatOperationList.Count, "Wrong count of records");

        }


    }
}