using Spectre.Console;
namespace LifeTracker;

public class UiController
{
    public void ShowStatsTable(DailyLog log, UserSettings settings)
    {
        var statsTable = new Table().Border(TableBorder.Square).BorderColor(Color.Orange1);

        statsTable.AddColumn("[grey]Stat[/]");
        statsTable.AddColumn("[grey]Value[/]");

        var totalsXp = DataCollectionService.CalculateTotalsXP(settings);

        foreach (var totalXp in totalsXp)
        {
            
            statsTable.AddRow(totalXp.Key, $"{LevelUpSystem.GetLevel(totalXp.Value).ToString()} ----- {(LevelUpSystem.GetLevel(totalXp.Value) +1).ToString()}");
        }
 
        AnsiConsole.Write(new Align(statsTable, HorizontalAlignment.Center));
    }
}