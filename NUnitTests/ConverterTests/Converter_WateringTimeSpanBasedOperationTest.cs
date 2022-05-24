namespace NUnitTests.ConverterTests
{
    internal class Converter_WateringTimeSpanBasedOperationTest
    {
        [Test]
        public void Test_ToSchedulerRecord_Watering_Intencity_High()
        {
            WateringTimeSpanBasedOperation item = new WateringTimeSpanBasedOperation()
            {
                Start = new TimeSpan(1, 0, 0),
                End = new TimeSpan(2,0,0) ,
                Amount = 100
            };
            
            DateTime startDate = DateTime.UtcNow;

            WateringSchedulerRecord schedulerRecord = item.ToSchedulerRecord(startDate);

            Assert.AreEqual(startDate + item.Start, schedulerRecord.StartDate,"Result StartDate  is incorrect");
            Assert.AreEqual(startDate + item.End, schedulerRecord.EndDate, "Result StartDate  is incorrect");
            Assert.AreEqual(item.Amount, schedulerRecord.Amount, "Result Intensity  is incorrect");
        }

        [Test]
        public void Test_ToSchedulerRecord_Watering_Intencity_off()
        {
            WateringTimeSpanBasedOperation item = new WateringTimeSpanBasedOperation()
            {
                Start = new TimeSpan(1, 0, 0),
                End = new TimeSpan(2, 1, 1),
                Amount = 2
            };
            
            DateTime startDate = new DateTime(2030,10,3,10,11,2);

            WateringSchedulerRecord schedulerRecord = item.ToSchedulerRecord(startDate);

            Assert.AreEqual(startDate + item.Start, schedulerRecord.StartDate, "Result StartDate  is incorrect");
            Assert.AreEqual(startDate + item.End, schedulerRecord.EndDate, "Result StartDate  is incorrect");
            Assert.AreEqual(item.Amount, schedulerRecord.Amount, "Result Intensity  is incorrect");
        }




    }
}
