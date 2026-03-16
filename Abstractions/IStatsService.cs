namespace LifeTracker;

public interface IStatsService
{
    void AddProgress(string key, double amount);
    void RemoveStat(string key);
    void RenameStat(string oldName, string newName);
    void UpdateWeight(string key, double weight);
    void AddNewStat(string name, double weight);
    IEnumerable<string> GetStatsList();
    double GetWeight(string name);
}