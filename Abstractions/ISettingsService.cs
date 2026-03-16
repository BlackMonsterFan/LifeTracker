using LifeTracker.Models;

namespace LifeTracker.Abstractions;

public interface ISettingsService
{
    void Save(UserSettings settings);
    UserSettings Load();
}