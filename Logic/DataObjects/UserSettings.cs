namespace LifeTracker;

public class UserSettings
{
    public Dictionary<string, double> Weights { get; set; } = new();

    public void UpdateWeight(string key, double weight)
    {
        Weights[key] = weight;
    }
}