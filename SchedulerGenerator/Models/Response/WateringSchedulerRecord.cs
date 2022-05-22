namespace SchedulerGerenrator.Models.Response
{
    public record WateringSchedulerRecord : SchedulerRecord
    {           
        public short Amount { get; set; }
    }
}
