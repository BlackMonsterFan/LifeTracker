namespace LifeTracker.Models;

public class DailyLog
{
    public DateTime Date {get; set;} = DateTime.Now.Date;
    public Dictionary<string, double> Stats { get; set; } = new();    
}