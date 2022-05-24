using SchedulerGenerator.Models.ExternalApi.IGS;

namespace SchedulerGenerator.Models.Operations
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
        
    }

}