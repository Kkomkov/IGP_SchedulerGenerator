using SchedulerGenerator.Models.Operations;
using SchedulerGenerator.Models.Operations;
using SchedulerGenerator.Models.Response;

namespace SchedulerGenerator.Services.Extensions
{
    public static class Converter
    {

        public static SchedulerRecord ToSchedulerRecord(this TimeSpanBasedOperation operation, DateTime startDate,SchedulerRecord schedulerRecord)
        {
            
            schedulerRecord.StartDate = startDate + operation.Start;
            schedulerRecord.EndDate = startDate + operation.End;

            return schedulerRecord;
        }
        public static LightingSchedulerRecord ToSchedulerRecord(this LightingTimeSpanBasedOperation operation, DateTime startDate)
        {
            var result = new LightingSchedulerRecord() { Intensity = operation.Intensity };
            operation.ToSchedulerRecord(startDate, result);

            return result;
        }
        public static WateringSchedulerRecord ToSchedulerRecord(this WateringTimeSpanBasedOperation operation, DateTime startDate)
        {

            var result = new WateringSchedulerRecord() { Amount = operation.Amount };
            operation.ToSchedulerRecord(startDate, result);

            return result;
        }


        public static TrayResponse ToSchedulerRecord(this FlatRecipe recipe, int trayNumber, DateTime startDate)
        {
            var trayResponse = new TrayResponse(trayNumber);

            trayResponse.Lighting = recipe.Lighting.Select(x => x.ToSchedulerRecord(startDate)).ToList();
            trayResponse.Watering = recipe.Watering.Select(x => x.ToSchedulerRecord(startDate)).ToList();

            return trayResponse;
        }



    }
}
