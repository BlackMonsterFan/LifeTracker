using System.Text.Json;
using LifeTracker.Abstractions;
using LifeTracker.Models;
using LifeTracker.Utils;
namespace LifeTracker.Services;

public class DataService(IUserNotifyer userNotifyer) : IDataService
{
    public DailyLog? Load(string date)
    {
        string filePath = Path.Combine(FileSystemConfig.LogsPath, $"{date}.json");
        return LoadByPath(filePath);
    }

    public IEnumerable<DailyLog> LoadAll()
    {
        string[] filePaths = Directory.GetFiles(FileSystemConfig.LogsPath, "*.json");

        if (filePaths.Length == 0) return Enumerable.Empty<DailyLog>();

        var logs = new List<DailyLog>();
        
        foreach (var path in filePaths)
        {
            var log = LoadByPath(path);

            if (log != null) logs.Add(log);
            
        }

        return logs.Where(log => log != null);
    }

    private DailyLog? LoadByPath(string path)
    {
        if (!File.Exists(path)) return null;

        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<DailyLog>(json);
        }
        catch (JsonException)
        {
            string corruptedPath = path + ".corrupted";
            File.Move(path, corruptedPath, overwrite: true);

            userNotifyer.Error($"Failed to load {path}. Check it manually at: {FileSystemConfig.LogsPath}");
            return null;
        }
        catch (Exception ex)
        {
            userNotifyer.Error($"Failed to load {path}. Error: {ex}");
            return null;
        }
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