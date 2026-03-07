using System;

namespace LifeTracker;

public class DailyLog
{
    public DateTime Date {get; set;} = DateTime.Now.Date;

    public double StudyHours {get; private set;}
    public int MonsterCount {get; private set;}
    public int CarbsCount {get; private set;}
    public bool GymVisited {get; private set;}

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