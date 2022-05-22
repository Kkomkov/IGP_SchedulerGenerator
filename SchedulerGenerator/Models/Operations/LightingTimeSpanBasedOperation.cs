using SchedulerGerenrator.Models.ExternalApi.IGS;

namespace SchedulerGerenrator.Models.Operations
{
    public record LightingTimeSpanBasedOperation : TimeSpanBasedOperation
    {
        public LightIntensity Intensity;

        public LightingTimeSpanBasedOperation() { }
        public LightingTimeSpanBasedOperation( TimeSpan start,TimeSpan end , LightIntensity intensity) {
            Start = start;
            End = end;
            Intensity = intensity;
        }
        //public  bool Equals(LightingTimeSpanBasedOperation? obj)
        //{
        //    return base.Equals(obj)&& Intensity==obj?.Intensity;
        //}

    }

}