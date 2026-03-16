using Spectre.Console;
using LifeTracker.Abstractions;
using LifeTracker.Logic;
using LifeTracker.Utils;


namespace LifeTracker.Services;

public class InputService : IInputService
{
    public T GetChoice<T>(string title) where T : struct, Enum
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<T>()
                .ApplySystemStyle($"{title}")
                .AddChoices(Enum.GetValues<T>())
                .UseConverter(option => option.GetDescription())
        );

        return choice;
    }

    public int AskInt(string prompt)
    {
        int number = AnsiConsole.Ask<int>(prompt);

        return number;
    }

    public (string, double) GetNewStatDetails()
    {
        string name = AnsiConsole.Ask<string>("[bold orange1]Ім'я статистики: [/]");
        double weight = AnsiConsole.Ask<double>("[bold orange1]Ціна за одинцю в Xp: [/]");

        return (name, weight);
    }

    public string GetStatName(IEnumerable<string> choices, string title)
    {
        var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .ApplySystemStyle(title)
        .AddChoices(choices)
        .AddChoices("[bold]🚪 Вихід[/]")
        );

        if (choice == "[bold]🚪 Вихід[/]") return "Вихід";
        return choice;
    }

    public bool GetConfirm(string title)
    {
        bool confirm = AnsiConsole.Confirm($"[bold]{title}[/]");
        return confirm;
    }
}