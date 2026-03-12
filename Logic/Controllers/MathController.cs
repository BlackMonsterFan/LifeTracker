using System.IO;
using System.Text.Json;

namespace LifeTracker;

public class MathController
{
    public static Dictionary<string, double> CalculateTotals(UserSettings settings)
    {
        string[] filePaths = Directory.GetFiles($"Data/Logs");

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
}

// знайти всі шляхи - пропарсити з джсон в дейлілог - підсумувати (всі значення які є в налаштуваннях) - 