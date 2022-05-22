namespace SchedulerGerenrator.Models.Response
{
    public record WaterSchedulerRecord : SchedulerRecord
    {           
        public short Amount { get; set; }
    }
}
