using System.IO;
using System.Text.Json;

namespace LifeTracker;

public class MathController
{
    public static Dictionary<string, double> TotalXp(UserSettings settings)
    {
        string[] filePaths = Directory.GetFiles($"Data/Logs");

        var xp = filePaths
        .Select(file => JsonSerializer.Deserialize<DailyLog>(File.ReadAllText(file)))
        .Where(log => log != null && log.Stats.Keys == settings.Weights.Keys)
        .Sum();


        var TotalXp = new Dictionary<string, double>();

        foreach (string k in settings.Weights.Keys)
        {
            
        }

        return TotalXp;
    }
}

// знайти всі шляхи - пропарсити з джсон в дейлілог - підсумувати (всі значення які є в налаштуваннях) - 