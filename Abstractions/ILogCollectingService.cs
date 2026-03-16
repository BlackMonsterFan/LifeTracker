namespace LifeTracker.Abstractions;

public interface ILogCollectingService
{
    Dictionary<string, double> CalculateTotalValues();
}