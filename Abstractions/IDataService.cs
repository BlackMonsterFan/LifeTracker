using LifeTracker.Models;

namespace LifeTracker.Abstractions;

public interface IDataService
{
    DailyLog? Load(string date);
    IEnumerable<DailyLog> LoadAll();
    void Save(DailyLog log);
}