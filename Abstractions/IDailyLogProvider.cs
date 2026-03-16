using LifeTracker.Models;

namespace LifeTracker.Abstractions;

public interface IDailyLogProvider
{
    public DailyLog GetOrInitialize(DateTime date);
}