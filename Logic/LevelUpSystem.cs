namespace LifeTracker;

public static class LevelUpSystem{

    private const int BaseXp = 500;
    private const double Exponent = 1.4;

    public static int GetLevel(double totalXp)
    {
        return (int)Math.Pow(totalXp / BaseXp, 1 / Exponent) + 1;
    }

    public static double GetXpRequirement(int level)
    {
        if (level <= 1) return 0;
        return BaseXp * Math.Pow(level - 1, Exponent);
    }
}