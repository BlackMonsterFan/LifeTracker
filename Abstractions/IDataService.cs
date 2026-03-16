namespace LifeTracker;

public interface IDataService
{
    DailyLog? Load(string date);
    IEnumerable<DailyLog> LoadAll();
    void Save(DailyLog log);
}