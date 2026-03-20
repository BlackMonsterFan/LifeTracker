using LifeTracker.Models;

namespace LifeTracker.Abstractions;

public interface IStatsService
{
    void AddProgress(Guid id, double amount);
    void RemoveStat(Guid id);
    void RenameStat(Guid id, string newName);
    void UpdateWeight(Guid id, double weight);
    void AddNewStat(string name, double weight);
    IEnumerable<StatDefinition> GetStatsList();
    double GetWeight(Guid id);
    Guid GetId(string name);
}