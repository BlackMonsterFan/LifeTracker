namespace LifeTracker.Abstractions;

public interface ILevelUpSystem
{
    double CalculateXp(double value, double weight);
    int GetLevel(double Xp);
    double GetXpRequirement(int level);
}