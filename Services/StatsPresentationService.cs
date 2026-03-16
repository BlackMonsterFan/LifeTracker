namespace LifeTracker.Services;
using LifeTracker.Models;
using LifeTracker.Abstractions;

public class StatsPresentationService (ILogCollectingService aggerator, ISettingsService settingsService, ILevelUpSystem levelUpSystem) : IStatsPresentationService
{
    public IEnumerable<StatInfo> GetStatsInfo()
    {
        var totals = aggerator.CalculateTotalValues();
        var setting = settingsService.Load();

        var statsInfo = new List<StatInfo>(setting.Weights.Count());

        foreach (var pair in totals)
        {
            string name = pair.Key;
            double value = pair.Value;

            double weight = setting.Weights[name];

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