namespace SchedulerGerenrator.Models.ExternalApi.IGS;

public record Recipe(
    string Name)
{
    public List<LightingPhase> LightingPhases { get; } = new List<LightingPhase>();
    public List<WateringPhase> WateringPhases { get; } = new List<WateringPhase>();
}

public abstract record Phase(string Name, short Order, short Hours, short Minutes, short Repetitions);

public record LightingPhaseOperation(short OffsetHours, short OffsetMinutes, LightIntensity LightIntensity);

public enum LightIntensity
{
    Off = 0,
    Low = 1,
    Medium = 2,
    High = 3
}

public record WateringPhase(string Name, short Order, short Hours, short Minutes, short Repetitions, short Amount)
    : Phase(Name, Order, Hours, Minutes, Repetitions);

public record LightingPhase(string Name, short Order, short Hours, short Minutes, short Repetitions)
    : Phase(Name, Order, Hours, Minutes, Repetitions)
{
    public IEnumerable<LightingPhaseOperation>? Operations { get; init; }
};

