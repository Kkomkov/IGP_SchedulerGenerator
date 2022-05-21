using SchedulerGerenrator.Models.ExternalApi.IGS;

namespace SchedulerGerenrator.Models.Response
{
    public abstract class SchedulerRecord
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class LightSchedulerRecord: SchedulerRecord
    {      
        public LightIntensity Intencity { get; set; }
    }
}
