using SchedulerGenerator.Models.ExternalApi.IGS;

namespace SchedulerGenerator.Models.Response
{
    public abstract record SchedulerRecord
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public record LightingSchedulerRecord: SchedulerRecord
    {      
        public LightIntensity Intensity { get; set; }
    }
}
