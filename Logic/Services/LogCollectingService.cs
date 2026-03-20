using LifeTracker.Abstractions;
namespace LifeTracker.Services;

public class LogCollectingService(IDataService data, ISettingsService settingsService) : ILogCollectingService
{
    public Dictionary<Guid, double> CalculateTotalValues()
    {
        var dailyLogs = data.LoadAll();
        var settings = settingsService.Load();

        var totals = new Dictionary<Guid, double>();

        foreach (Guid id in settings.Stats.Select(statDef => statDef.Id))
        {
            totals[id] = 0;   
        }

        foreach (var log in dailyLogs)
        {
            foreach (var stat in log.DailyStats)
            {
                if (totals.ContainsKey(stat.Key))
                {
                    totals[stat.Key] += stat.Value;
                }
            }
        }

        return totals;
    }
}
