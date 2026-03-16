namespace LifeTracker;

public class LogCollectingService(IDataService data, ISettingsService settingsService) : ILogCollectingService
{
    public Dictionary<string, double> CalculateTotalValues()
    {
        var dailyLogs = data.LoadAll();
        var settings = settingsService.Load();

        var totals = new Dictionary<string, double>();

        foreach (string key in settings.Weights.Keys)
        {
            totals[key] = 0;   
        }

        foreach (var log in dailyLogs)
        {
            foreach (var stat in log.Stats)
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
