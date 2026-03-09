using System;

namespace LifeTracker;

public class DailyLog
{
    public DateTime Date {get; set;} = DateTime.Now.Date;

    public Dictionary<string, double> Stats { get; set; } = new();

    public void UpdateStat(string key, double value)
    {
        if(Stats.ContainsKey(key)) Stats[key] += value;
        else Stats[key] = value;
    }
    
}