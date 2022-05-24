using SchedulerGenerator.Services.Extensions;
using SchedulerGerenrator.Models.Response;

namespace NUnitTests.ConverterTests
{
    internal class Converter_LightingTimeSpanBasedOperationTest
    {
        [Test]
        public void Test_ToSchedulerRecord_Lighting_Intencity_High()
        {
            LightingTimeSpanBasedOperation item = new LightingTimeSpanBasedOperation()
            {
                Start = new TimeSpan(1, 0, 0),
                End = new TimeSpan(2,0,0) ,
                Intensity = LightIntensity.High
            };
             new  LightingSchedulerRecord();
            DateTime startDate = DateTime.UtcNow;

            LightingSchedulerRecord schedulerRecord = item.ToSchedulerRecord(startDate);

            Assert.AreEqual(startDate + item.Start, schedulerRecord.StartDate,"Result StartDate  is incorrect");
            Assert.AreEqual(startDate + item.End, schedulerRecord.EndDate, "Result StartDate  is incorrect");
            Assert.AreEqual(item.Intensity, schedulerRecord.Intensity, "Result Intensity  is incorrect");
        }

        [Test]
        public void Test_ToSchedulerRecord_Lighting_Intencity_off()
        {
            LightingTimeSpanBasedOperation item = new LightingTimeSpanBasedOperation()
            {
                Start = new TimeSpan(1, 0, 0),
                End = new TimeSpan(2, 0, 0),
                Intensity = LightIntensity.High
            };
            new LightingSchedulerRecord();
            DateTime startDate = DateTime.UtcNow;

            LightingSchedulerRecord schedulerRecord = item.ToSchedulerRecord(startDate);

            Assert.AreEqual(startDate + item.Start, schedulerRecord.StartDate, "Result StartDate  is incorrect");
            Assert.AreEqual(startDate + item.End, schedulerRecord.EndDate, "Result StartDate  is incorrect");
            Assert.AreEqual(item.Intensity, schedulerRecord.Intensity, "Result Intensity  is incorrect");
        }




    }
}
