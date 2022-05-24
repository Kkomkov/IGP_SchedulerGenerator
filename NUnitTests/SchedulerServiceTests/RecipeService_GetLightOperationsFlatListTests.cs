namespace NUnitTests.SchedulerServiceTests
{
    public class RecipeService_GetLightOperationsFlatListTests
    {

       
        private RecipeManipulationService _recipeService;

        [SetUp]
        public void Setup()
        {
             _recipeService = new RecipeManipulationService(null, null);
          
        }

        private static IEnumerable<TestCaseData> TestDataGetOperationsFlatList =>
           new List<TestCaseData>
           {
                new TestCaseData( new Recipe("3") { },new  List<LightingTimeSpanBasedOperation> ()),
                new TestCaseData(

                                new Recipe("2") {
                                        LightingPhases = new List<LightingPhase>() {
                                                new LightingPhase("wp1",0,1,0,1)
                                                {
                                                    Operations = new List<LightingPhaseOperation>(){
                                                       new LightingPhaseOperation(0,0, LightIntensity.Low)
                                                    }
                                                }
                                        }
                                    },
                                //expected result  
                                new  List<LightingTimeSpanBasedOperation> () {
                                    new LightingTimeSpanBasedOperation(new TimeSpan(0,0,0),new TimeSpan(1,0,0),LightIntensity.Low)
                                }

                ),
                new TestCaseData (
                    new Recipe("1")  {

                        LightingPhases = new List<LightingPhase>() {
                            new LightingPhase("wp1",0,1,0,1)
                            {
                                Operations = new List<LightingPhaseOperation>(){
                                   new LightingPhaseOperation(0,0, LightIntensity.Low)
                                }
                            },
                            new LightingPhase("wp2",0,2,0,2)
                            {
                                Operations = new List<LightingPhaseOperation>(){
                                    new LightingPhaseOperation(0,0, LightIntensity.High)
                                }
                            },
                        }
                    },

                    new  List<LightingTimeSpanBasedOperation> () {
                        new LightingTimeSpanBasedOperation(new TimeSpan(0,0,0),new TimeSpan(1,0,0),LightIntensity.Low),
                        new LightingTimeSpanBasedOperation(new TimeSpan(1,0,0),new TimeSpan(3,0,0),LightIntensity.High),
                        new LightingTimeSpanBasedOperation(new TimeSpan(3,0,0),new TimeSpan(5,0,0),LightIntensity.High),
                    }
                )
           };


        [Test]
        public void TestHardcodedRecipe_1()
        {
            var recipe = new Recipe("1")
            {

                LightingPhases = new List<LightingPhase>() {
                            new LightingPhase("wp1", 0, 1, 0, 1)
                            {
                                Operations = new List<LightingPhaseOperation>(){
                                    new LightingPhaseOperation(0,0, LightIntensity.Low)
                                }
                            },
                            new LightingPhase("wp2", 1, 2, 0, 2)
                            {
                                Operations = new List<LightingPhaseOperation>(){
                                    new LightingPhaseOperation(0,0, LightIntensity.High)
                                }
                            },
                        }
            };


            var flatOperationList = _recipeService.GetLightOperationsFlatList(recipe);
            Assert.NotNull(flatOperationList, "Flat operations list should not be null");
            flatOperationList.OrderBy(x => x.Start);


            Assert.AreEqual(flatOperationList.Count, 3, "Wrong count of records");
            Assert.AreEqual(flatOperationList[0].Start, new TimeSpan(0, 0, 0)); Assert.AreEqual(flatOperationList[0].End, new TimeSpan(1, 0, 0)); Assert.AreEqual(flatOperationList[0].Intensity, LightIntensity.Low);
            Assert.AreEqual(flatOperationList[1].Start, new TimeSpan(1, 0, 0)); Assert.AreEqual(flatOperationList[1].End, new TimeSpan(3, 0, 0)); Assert.AreEqual(flatOperationList[1].Intensity, LightIntensity.High);
            Assert.AreEqual(flatOperationList[2].Start, new TimeSpan(3, 0, 0)); Assert.AreEqual(flatOperationList[2].End, new TimeSpan(5, 0, 0)); Assert.AreEqual(flatOperationList[2].Intensity, LightIntensity.High);

        }

        [Test, TestCaseSource(nameof(TestDataGetOperationsFlatList))]
        public void Test2(Recipe recipe, List<LightingTimeSpanBasedOperation> expectedList)
        {

            var flatOperationList = _recipeService.GetLightOperationsFlatList(recipe);

            Assert.NotNull(flatOperationList, "Flat operations list should not be null");
            Assert.AreEqual(expectedList.Count, flatOperationList.Count, "Wrong count of records");
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(flatOperationList[i], expectedList[i], $"Row {i} in result has wrong value");
            }
        }


    }
}