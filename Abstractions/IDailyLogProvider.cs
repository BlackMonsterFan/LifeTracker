namespace LifeTracker;

public interface IDailyLogProvider
{
    public DailyLog GetOrInitialize(DateTime date);
}