using SchedulerGenerator.Models.Operations;
using SchedulerGenerator.Services.Extensions;
using SchedulerGerenrator.Models.Response;

namespace NUnitTests.ConverterTests
{
    internal class Converter_FlatRecipe_ToSchedulerRecord_Tests
    {

        [Test]
        public void Test_ToSchedulerRecord_Watering_Intencity_High()
        {
            FlatRecipe recipe  = new FlatRecipe ("1")
            {
               Lighting =new List<LightingTimeSpanBasedOperation>()
               {
                    new LightingTimeSpanBasedOperation()
                    {
                        Start = new TimeSpan(1, 1, 1),
                        End = new TimeSpan(2,2,3) ,
                        Intensity = LightIntensity.High
                    }
               },
               Watering = new List<WateringTimeSpanBasedOperation>()
               {
                    new WateringTimeSpanBasedOperation()
                    {
                        Start = new TimeSpan(1, 2, 3),
                        End = new TimeSpan(2,2,5) ,
                        Amount = 100
                    }
               }
            };
            
            DateTime startDate = DateTime.UtcNow;
            int trayNumber = 1;

            TrayResponse result = recipe.ToSchedulerRecord(trayNumber,startDate);

            Assert.AreEqual(trayNumber,result.TrayNumber, "Result trayNumber  is incorrect");
            
            Assert.AreEqual(recipe.Lighting.Count, result.Lighting.Count, "result.Lighting.Count  is incorrect");
            Assert.AreEqual(recipe.Watering.Count, result.Watering.Count, "result.Watering.Count  is incorrect");

            Assert.AreEqual(startDate+ recipe.Lighting[0].Start, result.Lighting[0].StartDate, "result.Lighting[0].StartDate  is incorrect");
            Assert.AreEqual(startDate + recipe.Lighting[0].End, result.Lighting[0].EndDate, "result.Lighting[0].EndDate  is incorrect");

            Assert.AreEqual(recipe.Lighting[0].Intensity, result.Lighting[0].Intensity, "result.Lighting[0].Intensity  is incorrect");
            Assert.AreEqual(recipe.Watering[0].Amount, result.Watering[0].Amount, "result.Watering[0].Amount  is incorrect");



            Assert.AreEqual(startDate + recipe.Watering[0].Start, result.Watering[0].StartDate, "result.Watering[0].StartDate  is incorrect");
            Assert.AreEqual(startDate + recipe.Watering[0].End, result.Watering[0].EndDate, "result.Watering[0].EndDate  is incorrect");



        }




    }
}
