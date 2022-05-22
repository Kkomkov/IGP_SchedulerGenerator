using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Services;

namespace TestProject
{
    public class SchedulerServiceTests
    {
        public static IEnumerable<object[]> TestDataGetOperationsFlatList =>
            new List<object[]>
            {
                new object[] { 
                    new Recipe("1")  { 
                        LightingPhases= new List<LightingPhase>() {
                            new LightingPhase("p1",1,0,15,1) { 
                                Operations  = new List<LightingPhaseOperation>(){
                                        new LightingPhaseOperation(0,0,LightIntensity.Low),
                                        new LightingPhaseOperation(0,5,LightIntensity.High)
                                }
                             },
                            new LightingPhase("p2",2,1,0,1){
                                Operations  = new List<LightingPhaseOperation>(){
                                        new LightingPhaseOperation(0,0,LightIntensity.Low)                                        
                                }
                             },
                        } ,
                        WateringPhases = new List<WateringPhase>() {
                            new WateringPhase("wp1",0,1,0,1,10),
                            new WateringPhase("wp2",0,2,0,2,10),
                        }
                    }, 
                    3 , //expected  water operations count
                    3
                },
                new object[] { new Recipe("2") {
                    WateringPhases = new List<WateringPhase>() {
                            new WateringPhase("wp1",0,1,0,1,10)
                            
                        } }
                    ,1
                    ,0
                },
                new object[] { new Recipe("3") { },0,0 }
            };

        [Theory]
        [MemberData(nameof(TestDataGetOperationsFlatList))]
        public void Test1_GetWaterOperationsFlatList(Recipe recipe , int waterOperationCount, int lightOperationCount)
        {
            var _shedulerService = new SchedulerService();
            var flatOperationList = _shedulerService.GetWaterOperationsFlatList(recipe);
            Assert.NotNull( flatOperationList);
            Assert.Equal(waterOperationCount, flatOperationList.Count);
        }

        [Fact]
        public void Test2_GetWaterOperationsFlatList_CustomRecipe1()
        {
            var recipe = new Recipe("1")
            {
                
                WateringPhases = new List<WateringPhase>() {
                            new WateringPhase("wp1",0,1,0,1,10),
                            new WateringPhase("wp2",0,2,0,2,20),
                        }
            };

            var _shedulerService = new SchedulerService();
            var flatOperationList = _shedulerService.GetWaterOperationsFlatList(recipe);
            Assert.NotNull(flatOperationList);
            flatOperationList.OrderBy(x => x.Start);

            Assert.Equal(flatOperationList.Count, 3);
            Assert.Equal(flatOperationList[0].Start, new TimeSpan(0,0,0));   Assert.Equal(flatOperationList[0].End, new TimeSpan(1, 0, 0)); Assert.Equal(flatOperationList[0].Amount, 10);
            Assert.Equal(flatOperationList[1].Start, new TimeSpan(1, 0, 0)); Assert.Equal(flatOperationList[1].End, new TimeSpan(3, 0, 0)); Assert.Equal(flatOperationList[1].Amount, 20);
            Assert.Equal(flatOperationList[2].Start, new TimeSpan(3, 0, 0)); Assert.Equal(flatOperationList[2].End, new TimeSpan(5, 0, 0)); Assert.Equal(flatOperationList[2].Amount, 20);
        }

        [Theory]
        [MemberData(nameof(TestDataGetOperationsFlatList))]
        public void Test2_GetLightOperationsFlatList(Recipe recipe, int waterOperationCount, int lightOperationCount)
        {
            var _shedulerService = new SchedulerService();
            var flatOperationList = _shedulerService.GetLightOperationsFlatList(recipe);
            Assert.NotNull(flatOperationList);
            Assert.Equal(lightOperationCount, flatOperationList.Count);
        }

    }
}
