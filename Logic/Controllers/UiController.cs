using Spectre.Console;
using System.Text;
namespace LifeTracker;

public class UiController
{
    public void ShowStatsTable(DailyLog log, UserSettings settings)
    {
        string GetBar(double progress, int width = 30)
        {
            int filled = (int)(progress * width);
            var bar = new string('━', filled);
            var empty = new string('━', width - filled - 1);
            return $"[orange1]{bar}[/][white]╸[/][grey23]{empty}[/]";
        }

        var totalsXp = DataCollectionService.CalculateTotalsXP(settings);
        double total = totalsXp.Values.Sum();

        var table = new Table().Border(TableBorder.Square).BorderColor(Color.Orange1);

        table.AddColumn($"[bold]Your totals: [/]");
        table.AddColumn($"[white]{total}Xp[/]", col => col.Centered());

        foreach (var totalXp in totalsXp)
        {
            int level = LevelUpSystem.GetLevel(totalXp.Value);
            double neededXp = LevelUpSystem.GetXpRequirement(level + 1);

            double progress = totalXp.Value / neededXp;

            table.AddRow(
                new Rows(new Markup($"[bold]{totalXp.Key}[/]"), new Markup($"[dim]current lvl:[/] [orange1]{level}[/]")),
                new Rows(new Markup($"{Math.Round(totalXp.Value)} / {Math.Round(neededXp)}"), new Markup(GetBar(progress)))
            );

            table.AddEmptyRow();
        }
 
        AnsiConsole.Write(new Align(table, HorizontalAlignment.Center));
    }
}

public static class PromptExtensions
{
    public static SelectionPrompt<T> ApplySystemStyle<T>(
        this SelectionPrompt<T> prompt, 
        string title, 
        int pageSize = 10) where T : notnull
    {

        return prompt
            .Title($"[bold grey]{title}[/]")
            .PageSize(pageSize)
            .HighlightStyle(new Style(Color.Orange1, Color.Black, Decoration.Bold))
            .MoreChoicesText("[grey](Стрілки - навігація, Enter - вибір)[/]");
    }
}