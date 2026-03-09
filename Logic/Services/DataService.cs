using System.IO;
using System.Text.Json;

namespace LifeTracker;

public class DataService
{
    public void Save(DailyLog log)
    {
        string fileName = $"Data/Logs/{DateTime.Now:yyyy-MM-dd}.json";
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(log, options);
        File.WriteAllText(fileName, json);
    }

    public DailyLog Load()
    {
        string fileName = $"Data/Logs/{DateTime.Now:yyyy-MM-dd}.json";

        if (!File.Exists(fileName))
        {
            return new DailyLog();
        }

        string json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<DailyLog>(json) ?? new DailyLog();
    }
}