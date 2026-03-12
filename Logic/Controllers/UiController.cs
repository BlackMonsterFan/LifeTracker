using Spectre.Console;
namespace LifeTracker;

public class UiController
{
    public void ShowStatsTable(DailyLog log, UserSettings settings)
    {
        var statsTable = new Table().Border(TableBorder.Square).BorderColor(Color.Orange1);

        statsTable.AddColumn("[grey]Stat[/]");
        statsTable.AddColumn("[grey]Value[/]");

        var test = MathController.CalculateTotals(settings);

        foreach (var k in test)
        {
            
            statsTable.AddRow(k.Key, k.Value.ToString());
        }
 
        AnsiConsole.Write(new Align(statsTable, HorizontalAlignment.Center));
    }
}