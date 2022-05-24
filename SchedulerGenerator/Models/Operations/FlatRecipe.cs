using SchedulerGerenrator.Models.Operations;

namespace SchedulerGenerator.Models.Operations
{
    public class FlatRecipe
    {
        public string Name { get; private set; }
        public List<LightingTimeSpanBasedOperation> Lighting { get;  set; } = new List<LightingTimeSpanBasedOperation>();
        public List<WateringTimeSpanBasedOperation> Watering { get;  set; } = new List<WateringTimeSpanBasedOperation>();
        public FlatRecipe(string name)
        {
            Name = name;            
        }
    }
}
