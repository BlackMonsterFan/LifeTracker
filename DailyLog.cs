using System;

namespace LifeTracker;

public class DailyLog
{
    public DateTime Date {get; set;} = DateTime.Now.Date;

    public double StudyHours {get; set;}
    public int MonsterCount {get; set;}
    public int CarbsCount {get; set;}
    public bool GymVisited {get; set;}

    public double IntelligentXP => StudyHours * 100;

    public void AddStudyTime(double time)
    {
        StudyHours += time;
    } 

    public void AddMonster()
    {
        MonsterCount++;
    }

    public void SetGymStatus(bool status)
    {
        GymVisited = status;
    }

    public void AddCarbs(int amount)
    {
        CarbsCount += amount;
    }
}