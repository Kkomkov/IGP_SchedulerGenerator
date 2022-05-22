

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
                     new List<LightSchedulerRecord> (){ }
                    ),
                new TestCaseData(
                     new List<LightingTimeSpanBasedOperation> (){ 
                            new LightingTimeSpanBasedOperation() { Start = new TimeSpan(0,0,0),End= new TimeSpan(0,0,0),Amount =0} 
                     },
                     new DateTime(2000,1,1),
                     new List<LightSchedulerRecord> (){
                          new LightSchedulerRecord() { StartDate =new DateTime(2000,1,1)+ new TimeSpan(0,0,0),EndDate= new DateTime(2000,1,1)+new TimeSpan(0,0,0),Amount =0}
                     }
                    ),
            };
        
        
        [Test, TestCaseSource(nameof(TestDataLightingTimeSpanBasedOperation))]
        public void Test_SeweralCases_ExpectPass(List<LightingTimeSpanBasedOperation> operations,DateTime startDate, List<LightSchedulerRecord> expectedResult)
        {
            
             var result = _shedulerService.GetLightScheduler(operations, startDate);
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, operations.Count);

            for( int i =0; i< expectedResult.Count;i++)
            {
                Assert.AreEqual(expectedResult[i], result[i],$"result record {i} is not correct");
            }
        }

        [Test]
        public void Test_nullOperations_expecteNull()
        {

            var result = _shedulerService.GetLightScheduler(null, DateTime.Now);
            Assert.Null(result);
        }

        [Test]
        public void Test_EmptyOperations_expecteEmpty()
        {

            var result = _shedulerService.GetLightScheduler(new List<LightingTimeSpanBasedOperation>(), DateTime.Now);
            Assert.NotNull(result);
            Assert.AreEqual(0,result.Count);
        }

    }
}
