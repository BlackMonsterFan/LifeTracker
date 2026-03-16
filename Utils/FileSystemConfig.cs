namespace LifeTracker.Utils;

public static class FileSystemConfig
{
    private static readonly string BasePath = AppContext.BaseDirectory;

    public static string LogsPath => Path.Combine(BasePath, "data", "logs");
    public static string SettingsPath => Path.Combine(BasePath, "data", "settings.json");

    public static void EnsureDirectoriesCreated()
    {
        if (!Directory.Exists(LogsPath))
        {
            Directory.CreateDirectory(LogsPath);
        }
    }
}