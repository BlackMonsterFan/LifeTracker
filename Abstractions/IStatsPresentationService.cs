namespace LifeTracker.Abstractions;
using LifeTracker.Models;

public interface IStatsPresentationService
{
    IEnumerable<StatInfo> GetStatsInfo();
}