using LifeTracker.Abstractions;
using LifeTracker.Models;
namespace LifeTracker.Services;

public class StatsService(IDataService data, ISettingsService settingsData, IDailyLogProvider logProvider) : IStatsService
{
    public void AddProgress(Guid id, double amount)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);

        log.DailyStats[id] += amount;

        data.Save(log);
    }

    public void RemoveStat(Guid id)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);
        var settings = settingsData.Load();

        log.DailyStats.Remove(id);

        settings.Stats.Remove(settings.Stats.Find(statDef => statDef.Id == id));

        data.Save(log);
        settingsData.Save(settings);
    }

    public void RenameStat(Guid id, string newName)
    {
        var settings = settingsData.Load();

        var index = settings.Stats.FindIndex(statDef => statDef.Id == id);

        if (index != -1)
        {
            settings.Stats[index] = settings.Stats[index] with {Name = newName};
        }

        settingsData.Save(settings);
    }

    public void UpdateWeight(Guid id, double weight)
    {
        var settings = settingsData.Load();

        int index = settings.Stats.FindIndex(statDef => statDef.Id == id);

        if (index != -1)
        {
            settings.Stats[index] = settings.Stats[index] with {Weight = weight};
        }

        settingsData.Save(settings);
    }

    public void AddNewStat(string name, double weight)
    {
        var log = logProvider.GetOrInitialize(DateTime.Now);
        var settings = settingsData.Load();

        var id = Guid.NewGuid();

        var newStat = new StatDefinition
        (
            id,
            name,
            weight
        );

        log.DailyStats[id] = 0;
        settings.Stats.Add(newStat);

        data.Save(log);
        settingsData.Save(settings);
    }

    public IEnumerable<StatDefinition> GetStatsList()
    {
        var settings = settingsData.Load();

        return settings.Stats;
    }

    public double GetWeight(Guid id)
    {
        var setting = settingsData.Load();
        return setting.Stats.Find(statDef => statDef.Id == id).Weight;
    } 

    public Guid GetId(string name)
    {
        var settings = settingsData.Load();
        return settings.Stats.Find(statDef => statDef.Name == name).Id;
    }
}