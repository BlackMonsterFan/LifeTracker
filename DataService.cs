using System.IO;
using System.Text.Json;

namespace LifeTracker;

class DataService()
{
    public static void Save(DailyLog log)
    {
        string fileName = $"data/{DateTime.Now:yyyy-MM-dd}.json";
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(log, options);
        File.WriteAllText(fileName, json);
    }

    public static DailyLog Load()
    {
        string fileName = $"data/{DateTime.Now:yyyy-MM-dd}.json";

        string json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<DailyLog>(json) ?? new DailyLog();
    }
}