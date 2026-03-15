using System.Text.Json;

namespace LifeTracker;

public class DataCollectionService
{
    public static Dictionary<string, double> CalculateTotals(UserSettings settings)
    {
        string[] filePaths = Directory.GetFiles(FileSystemConfig.LogsPath);

        var logs = filePaths
        .Select(file => JsonSerializer.Deserialize<DailyLog>(File.ReadAllText(file)))
        .Where(log => log != null);

        var Totals = new Dictionary<string, double>();

        foreach (string k in settings.Weights.Keys)
        {
            Totals[k] = 0;   
        }

        foreach (var log in logs)
        {
            foreach (var stat in log.Stats)
            {
                if (Totals.ContainsKey(stat.Key))
                {
                    Totals[stat.Key] += stat.Value;
                }
            }
        }

        return Totals;
    }

    public static Dictionary<string, double> CalculateTotalsXP(UserSettings settings)
    {
        var totals = CalculateTotals(settings);

        foreach(var stat in totals)
        {
            totals[stat.Key] = stat.Value * settings.Weights[stat.Key];
        }

        return totals;
    }
}
