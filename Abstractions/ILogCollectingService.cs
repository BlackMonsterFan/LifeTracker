namespace LifeTracker.Abstractions;

public interface ILogCollectingService
{
    Dictionary<Guid, double> CalculateTotalValues();
}