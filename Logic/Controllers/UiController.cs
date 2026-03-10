using Spectre.Console;
namespace LifeTracker;

public class UiController
{
    public void ShowStatsTable(DailyLog log)
    {
        var statsTable = new Table().Border(TableBorder.Square).BorderColor(Color.Orange1);

        statsTable.AddColumn("[grey]Stat[/]");
        statsTable.AddColumn("[grey]Value[/]");

        foreach (string k in log.Stats.Keys)
        {
            statsTable.AddRow(k);
            statsTable.AddRow("---------");
        }
 
        AnsiConsole.Write(new Align(statsTable, HorizontalAlignment.Center));
    }
}