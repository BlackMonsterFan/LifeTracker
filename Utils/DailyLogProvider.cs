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

        DailyLog newLog = new DailyLog {Date = date.Date};

        foreach (string key in settings.Weights.Keys)
        {
            newLog.Stats[key] = 0;
        }

        data.Save(newLog);

        return newLog;
    }
}