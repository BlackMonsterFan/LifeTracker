using System.Text.Json;
namespace LifeTracker;

public class DataService : IDataService
{
    public DailyLog? Load(string date)
    {
        string fileName = Path.Combine(FileSystemConfig.LogsPath, $"{date}.json");

        if (!File.Exists(fileName)) return null;

        string json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<DailyLog>(json) ?? new DailyLog();
    }

    public IEnumerable<DailyLog> LoadAll()
    {
        string[] filePaths = Directory.GetFiles(FileSystemConfig.LogsPath);

        if (filePaths.Length == 0) return Enumerable.Empty<DailyLog>();

        return filePaths
        .Select(file => JsonSerializer.Deserialize<DailyLog>(File.ReadAllText(file)))
        .Where(log => log != null)!;
    }

    public void Save(DailyLog log)
    {
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        string fileName = Path.Combine(FileSystemConfig.LogsPath, $"{currentDate}.json");
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(log, options);
        File.WriteAllText(fileName, json);
    }
}