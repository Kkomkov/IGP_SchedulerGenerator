

using SchedulerGerenrator.Models.Response;

namespace NUnitTests.SchedulerServiceTests
{
    public  class GetWaterSchedulerTests
    {
        private SchedulerService _shedulerService;

        [SetUp]
        public void Setup()
        {
            _shedulerService = new SchedulerService();
        }

        private static IEnumerable<TestCaseData> TestDataWateringTimeSpanBasedOperation =>
            new List<TestCaseData>
            { 
                new TestCaseData(
                     new List<WateringTimeSpanBasedOperation> (){ },
                     new DateTime(2000,1,1),
                     new List<WaterSchedulerRecord> (){ }
                    ),
                new TestCaseData(
                     new List<WateringTimeSpanBasedOperation> (){ 
                            new WateringTimeSpanBasedOperation() { Start = new TimeSpan(0,0,0),End= new TimeSpan(0,0,0),Amount =0} 
                     },
                     new DateTime(2000,1,1),
                     new List<WaterSchedulerRecord> (){
                          new WaterSchedulerRecord() { StartDate =new DateTime(2000,1,1)+ new TimeSpan(0,0,0),EndDate= new DateTime(2000,1,1)+new TimeSpan(0,0,0),Amount =0}
                     }
                    ),
            };
        
        
        [Test, TestCaseSource(nameof(TestDataWateringTimeSpanBasedOperation))]
        public void Test_SeweralCases_ExpectPass(List<WateringTimeSpanBasedOperation> operations,DateTime startDate, List<WaterSchedulerRecord> expectedResult)
        {
            
             var result = _shedulerService.GetWaterScheduler(operations, startDate);
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

            var result = _shedulerService.GetWaterScheduler(null, DateTime.Now);
            Assert.Null(result);
        }

        [Test]
        public void Test_EmptyOperations_expecteEmpty()
        {

            var result = _shedulerService.GetWaterScheduler(new List<WateringTimeSpanBasedOperation>(), DateTime.Now);
            Assert.NotNull(result);
            Assert.AreEqual(0,result.Count);
        }

    }
}
