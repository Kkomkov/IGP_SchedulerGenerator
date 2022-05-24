namespace NUnitTests.ConverterTests
{
    internal class Converter_TimeSpanBasedOperationTest
    {
        [Test]
        public void Test_ToSchedulerRecord_Lighting()
        {
            TimeSpanBasedOperation item = new LightingTimeSpanBasedOperation()
            {
                Start = new TimeSpan(1, 0, 0),
                End = new TimeSpan(2,0,0) 
            };
            SchedulerRecord schedulerRecord = new  LightingSchedulerRecord();
            DateTime startDate = DateTime.UtcNow;
            
            item.ToSchedulerRecord(startDate, schedulerRecord);

            Assert.AreEqual(startDate + item.Start, schedulerRecord.StartDate,"Result StartDate  is incorrect");
            Assert.AreEqual(startDate + item.End, schedulerRecord.EndDate, "Result StartDate  is incorrect");

        }


        [Test]
        public void Test_ToSchedulerRecord_Watering()
        {
            TimeSpanBasedOperation item = new WateringTimeSpanBasedOperation()
            {
                Start = new TimeSpan(1, 0, 0),
                End = new TimeSpan(2, 0, 0)
            };
            SchedulerRecord schedulerRecord = new WateringSchedulerRecord();
            DateTime startDate = DateTime.UtcNow;

            item.ToSchedulerRecord(startDate, schedulerRecord);

            Assert.AreEqual(startDate + item.Start, schedulerRecord.StartDate, "Result StartDate  is incorrect");
            Assert.AreEqual(startDate + item.End, schedulerRecord.EndDate, "Result StartDate  is incorrect");

        }


        [Test]
        public void Test_ToSchedulerRecord_Watering2()
        {
            TimeSpanBasedOperation item = new LightingTimeSpanBasedOperation()
            {
                Start = new TimeSpan(1, 0, 0),
                End = new TimeSpan(2, 0, 0)
            };
            SchedulerRecord schedulerRecord = new WateringSchedulerRecord();
            DateTime startDate = DateTime.UtcNow;

            item.ToSchedulerRecord(startDate, schedulerRecord);

            Assert.AreEqual(startDate + item.Start, schedulerRecord.StartDate, "Result StartDate  is incorrect");
            Assert.AreEqual(startDate + item.End, schedulerRecord.EndDate, "Result StartDate  is incorrect");

        }

    }
}
