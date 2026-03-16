namespace LifeTracker;

public interface ILogCollectingService
{
    Dictionary<string, double> CalculateTotalValues();
}