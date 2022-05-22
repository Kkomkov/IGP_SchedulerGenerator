using SchedulerGerenrator.Models.ExternalApi.IGS;

namespace SchedulerGerenrator.Models.Response
{
    public abstract record SchedulerRecord
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public record LightSchedulerRecord: SchedulerRecord
    {      
        public LightIntensity Intencity { get; set; }
    }
}
