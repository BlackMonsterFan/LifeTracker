using System.Text.Json;
namespace LifeTracker;

public class SettingsService
{
    public void Save(UserSettings settings)
    {
        string fileName = $"Data/Settings.json";
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(settings, options);
        File.WriteAllText(fileName, json);
    }

    public UserSettings Load()
    {
        string fileName = $"Data/Settings.json";

        if (!File.Exists(fileName))
        {
            return new UserSettings();
        }

        string json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
    }
}