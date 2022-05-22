

using SchedulerGerenrator.Models.Response;

namespace NUnitTests.SchedulerServiceTests
{
    public  class GetLightSchedulerTests
    {
        private SchedulerService _shedulerService;

        [SetUp]
        public void Setup()
        {
            _shedulerService = new SchedulerService();
        }

        private static IEnumerable<TestCaseData> TestDataLightingTimeSpanBasedOperation =>
            new List<TestCaseData>
            { 
                new TestCaseData(
                     new List<LightingTimeSpanBasedOperation> (){ },
                     new DateTime(2000,1,1),
                     new List<LightingSchedulerRecord> (){ }
                    ),
                new TestCaseData(
                     new List<LightingTimeSpanBasedOperation> (){ 
                            new LightingTimeSpanBasedOperation() { Start = new TimeSpan(0,0,0),End= new TimeSpan(0,0,0),Intensity =LightIntensity.Low} 
                     },
                     new DateTime(2000,1,1),
                     new List<LightingSchedulerRecord> (){
                          new LightingSchedulerRecord() { StartDate =new DateTime(2000,1,1)+ new TimeSpan(0,0,0),EndDate= new DateTime(2000,1,1)+new TimeSpan(0,0,0), Intensity =LightIntensity.Low}
                     }
                    ),
            };
        
        
        [Test, TestCaseSource(nameof(TestDataLightingTimeSpanBasedOperation))]
        public void Test_SeweralCases_ExpectPass(List<LightingTimeSpanBasedOperation> operations,DateTime startDate, List<LightingSchedulerRecord> expectdResult)
        {
            
             var result = _shedulerService.GetLightingScheduler(operations, startDate);
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, operations.Count);

            for( int i =0; i< expectdResult.Count;i++)
            {
                Assert.AreEqual(expectdResult[i], result[i],$"result record {i} is not correct");
            }
        }

        [Test]
        public void Test_nullOperations_expectNull()
        {

            var result = _shedulerService.GetLightingScheduler(null, DateTime.Now);
            Assert.Null(result);
        }

        [Test]
        public void Test_EmptyOperations_expectEmpty()
        {

            var result = _shedulerService.GetLightingScheduler(new List<LightingTimeSpanBasedOperation>(), DateTime.Now);
            Assert.NotNull(result);
            Assert.AreEqual(0,result.Count);
        }

    }
}
