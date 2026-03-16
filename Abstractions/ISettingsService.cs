namespace LifeTracker;

public interface ISettingsService
{
    void Save(UserSettings settings);
    UserSettings Load();
}