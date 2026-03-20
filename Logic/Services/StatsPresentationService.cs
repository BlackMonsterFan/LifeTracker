namespace LifeTracker.Services;
using LifeTracker.Models;
using LifeTracker.Abstractions;

public class StatsPresentationService (ILogCollectingService aggerator, ISettingsService settingsService, ILevelUpSystem levelUpSystem) : IStatsPresentationService
{
    public IEnumerable<StatInfo> GetStatsInfo()
    {
        var totals = aggerator.CalculateTotalValues();
        var setting = settingsService.Load();

        var statsInfo = new List<StatInfo>();

        foreach (var pair in totals)
        {
            var id = pair.Key;
            var value = pair.Value;
            
            var name = setting.Stats.Find(statDef => statDef.Id == id).Name;
            var weight = setting.Stats.Find(statDef => statDef.Id == id).Weight;

            var Xp = levelUpSystem.CalculateXp(value, weight);
            var level = levelUpSystem.GetLevel(Xp);
            var neededXp = levelUpSystem.GetXpRequirement(level + 1);

            var progress = Xp / neededXp;

            statsInfo.Add(new StatInfo(
                name,
                Xp,
                level,
                neededXp,
                progress
            ));
        }

        return statsInfo;
    }
}