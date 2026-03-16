using LifeTracker.Models;
using Spectre.Console;

namespace LifeTracker.Logic;

public class UiController
{
    public void ShowStatsTable(IEnumerable<StatInfo> stats)
    {
        var table = new Table().Border(TableBorder.Square).BorderColor(Color.Gray);

        table.AddColumn($"[bold]Your totals: [/]");
        table.AddColumn($"[white]{10}Xp[/]", col => col.Centered());

        foreach (var stat in stats)
        {
            table.AddRow(
                new Rows(new Markup($"[bold]{stat.Name}[/]"), new Markup($"[dim]current lvl:[/] [orange1]{stat.Level}[/]")),
                new Rows(new Markup($"{Math.Round(stat.CurrentXp)} / {Math.Round(stat.NeededXp)}"), new Markup(GetBar(stat.Progress)))
            );

            table.AddEmptyRow();
        }
 
        AnsiConsole.Write(new Align(table, HorizontalAlignment.Center));
    }

    public string GetBar(double progress)
    {
        int width = 30;
        int filled = (int)(progress * width);
        var bar = new string('━', filled);
        var empty = new string('━', width - filled - 1);
        return $"[orange1]{bar}[/][white]╸[/][grey23]{empty}[/]";
    }

    public void Clear()
    {
        AnsiConsole.Clear();
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