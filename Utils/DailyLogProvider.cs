using LifeTracker.Models;
using LifeTracker.Abstractions;

namespace LifeTracker.Utils;


public class DailyLogProvider(IDataService data, ISettingsService settingsData) : IDailyLogProvider
{
    public DailyLog GetOrInitialize(DateTime date)
    {
        string dateStr = date.ToString("yyyy-MM-dd");
        var existingLog = data.Load(dateStr);

        if (existingLog != null) return existingLog;

        var settings = settingsData.Load();
        var DailyStats = new Dictionary<Guid, double>();

        var newLog = new DailyLog 
        (
            date.Date,
            DailyStats
        );

        foreach (Guid Id in settings.Stats.Select(s => s.Id))
        {
            DailyStats[Id] = 0;
        }

        data.Save(newLog);

        return newLog;
    }
}