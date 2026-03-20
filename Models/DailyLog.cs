namespace LifeTracker.Models;

public record DailyLog (DateTime Date, Dictionary<Guid, double> DailyStats);