using System.Text.Json;
using LifeTracker.Abstractions;
using LifeTracker.Models;
using LifeTracker.Utils;

namespace LifeTracker.Services;

public class SettingsService : ISettingsService
{
    public void Save(UserSettings settings)
    {
        string json = JsonSerializer.Serialize(settings);
        File.WriteAllText(FileSystemConfig.SettingsPath, json);
    }

    public UserSettings Load()
    {
        if (!File.Exists(FileSystemConfig.SettingsPath))
        {
            return new UserSettings();
        }

        string json = File.ReadAllText(FileSystemConfig.SettingsPath);
        return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
    }
}