using LifeTracker.Abstractions;
namespace LifeTracker.Services;

public class StatsService(IDataService data, ISettingsService settingsData, IDailyLogProvider logProvider) : IStatsService
{
    public void AddProgress(string key, double amount)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);

        log.Stats[key] += amount;

        data.Save(log);
    }

    public void RemoveStat(string key)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);
        var settings = settingsData.Load();

        log.Stats.Remove(key);
        settings.Weights.Remove(key);

        data.Save(log);
        settingsData.Save(settings);
    }

    public void RenameStat(string oldName, string newName)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);
        var settings = settingsData.Load();

        log.Stats.Remove(oldName, out double value);
        settings.Weights.Remove(oldName, out double weight);

        log.Stats[newName] = value;
        settings.Weights[newName] = weight;

        data.Save(log);
        settingsData.Save(settings);
    }

    public void UpdateWeight(string key, double weight)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);
        var settings = settingsData.Load();

        settings.Weights[key] = weight;
        settingsData.Save(settings);
    }

    public void AddNewStat(string name, double weight)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);
        var settings = settingsData.Load();

        log.Stats[name] = 0;
        settings.Weights[name] = weight;

        data.Save(log);
        settingsData.Save(settings);
    }

    public IEnumerable<string> GetStatsList()
    {
        var settings = settingsData.Load();

        return settings.Weights.Keys;
    }

    public double GetWeight(string name)
    {
        var setting = settingsData.Load();
        return setting.Weights[name];
    }
}