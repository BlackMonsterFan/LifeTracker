using LifeTracker.Abstractions;
namespace LifeTracker.Logic;

public class LevelUpSystem : ILevelUpSystem
{
    private const int BaseXp = 500;
    private const double Exponent = 1.4;

    public double CalculateXp(double value, double weight)
    {
        return value * weight;
    }

    public int GetLevel(double Xp)
    {
        if (Xp <= 0) return 1;

        return (int)Math.Pow(Xp / BaseXp, 1 / Exponent) + 1;
    }

    public double GetXpRequirement(int level)
    {
        if (level <= 1) return 0;
        return BaseXp * Math.Pow(level - 1, Exponent);
    }
}