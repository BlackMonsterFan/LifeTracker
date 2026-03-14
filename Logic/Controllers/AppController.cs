namespace LifeTracker;
using Spectre.Console;

public class AppController(DailyLog currentLog, UserSettings settings, DataService dataService, SettingsService settingService, UiController ui)
{
    private DailyLog _currentLog = currentLog;
    private UserSettings _settings = settings;
    private DataService _dataService = dataService;
    private SettingsService _settingsService = settingService;
    private UiController _ui = ui;

    public enum MenuOption
    {
        Add,
        Delete,
        Settings,
        Exit
    }   

    public MenuOption ShowMainMenu()
    {
        AnsiConsole.Write(new FigletText("LIFE TRACKER").Color(Color.Orange1));
        ui.ShowStatsTable(currentLog, settings);

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOption>()
                .ApplySystemStyle("Оберіть опцію:")
                .AddChoices(Enum.GetValues<MenuOption>())
                .UseConverter(option => option switch {
                    MenuOption.Add => "󰐕 Додати значення",
                    MenuOption.Delete => "󰆴 Видалити значення",
                    MenuOption.Settings => "⚙️ Налаштування",
                    MenuOption.Exit => "󰈆 Вихід",
                    _ => option.ToString()
                }));

        return choice;
    }

    public void ChoiceHandler(MenuOption choice)
    {
        switch (choice)
        {
            case MenuOption.Add:
                AddValueMenu();
                break;

            case MenuOption.Delete:
                DeleteValueMenu();
                break;

            case MenuOption.Settings:
                var settingsChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .ApplySystemStyle("Оберіть опцію:")
                .AddChoices("Створити нову статистику", "Відредагувати статистику", "Видалити статистику", "[bold]󰈆 Вихід[/]")
                );

                SettingHandler(settingsChoice);

                break;
        
                case MenuOption.Exit:
                AnsiConsole.Clear();
                Environment.Exit(0);
                break;
        }
    }

    public void AddValueMenu()
    {
        var key = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .ApplySystemStyle("[bold orange1] Записати в: [/]")
                .AddChoices(currentLog.Stats.Keys)
                .AddChoices("[bold]󰈆 Вихід[/]")
        );

        if (key != "󰈆 Вихід")
        {
            AddValue(key);
        }
    }

    public void AddValue(string key)
    {
        int amount = AnsiConsole.Ask<int>($"[bold orange1] Введіть кількість одиниць: [/] \n [dim] 1 = {settings.Weights[key]}Xp: [/]");

        currentLog.UpdateStat(key, amount);
        dataService.Save(currentLog);
    }

    public void DeleteValueMenu()
    {
        var key = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .ApplySystemStyle("[bold orange1] Видалити з: [/]")
                .AddChoices(currentLog.Stats.Keys)
                .AddChoices("[bold]󰈆 Вихід[/]")
        );

        if (key != "󰈆 Вихід")
        {
            DeleteValue(key);
        }
    }

    public void DeleteValue(string key)
    {
        int amount = AnsiConsole.Ask<int>($"[bold orange1] Введіть кількість одиниць: [/] \n [dim] 1 = {settings.Weights[key]}Xp: [/]");

        currentLog.UpdateStat(key, -amount);
        dataService.Save(currentLog);
    }

    public void SettingHandler(string choice)
    {
        switch (choice)
        {
            case "Створити нову статистику":
                AddNewStat();
                break;

            case "Відредагувати статистику":
                EditStat();
                break;

            case "Видалити статистику":
                DeleteStat();
                break;

            case "[bold]󰈆 Вихід[/]":
                AnsiConsole.Clear();
                break;
        }
    }

    public void AddNewStat()
    {
        string key = AnsiConsole.Ask<string>("[bold orange1]Name for the stat?[/]");
        double weight = AnsiConsole.Ask<double>("[bold orange1]How much XP it gives?[/]");

        currentLog.UpdateStat(key, 0);
        settings.Update(key, weight);
        dataService.Save(currentLog);
        settingService.Save(settings);
    }

    public void EditStat() 
    {
        var key = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .ApplySystemStyle("Оберіть що відредагувати:")
        .AddChoices(currentLog.Stats.Keys)
        .AddChoices("[bold]󰈆 Вихід[/]")
        );

        if (key != "[bold]󰈆 Вихід[/]")
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .ApplySystemStyle("Оберіть що змінити:")
            .AddChoices("Ім'я", "XP ціну", "[bold]󰈆 Вихід[/]")
            );

            switch (choice)
            {
                case "Ім'я":
                    string newName = AnsiConsole.Ask<string>($"Введіть нове ім'я: (минуле: {key})");
                    bool confirmName = AnsiConsole.Confirm("Підтвердити зміну?");

                    if (confirmName)
                    {
                        currentLog.Stats.Remove(key, out double statQuantity);
                        currentLog.UpdateStat(newName, statQuantity);
                        dataService.Save(currentLog);

                        settings.Weights.Remove(key, out double weightQuantity);
                        settings.Update(newName, weightQuantity);
                        settingService.Save(settings);

                    }else break; 
                    break;
                    
                case "XP ціну":
                    double newValue = AnsiConsole.Ask<double>($"Введіть нову ціну: (минула: {settings.Weights[key]})");
                    bool confirmValue = AnsiConsole.Confirm("Підтвердити зміну?");

                    if (confirmValue)
                    {
                        settings.Update(key, newValue);
                        settingService.Save(settings);

                    }else break; 
                    break;

                case "[bold]󰈆 Вихід[/]":
                    break;
            }
        }
    }

    public void DeleteStat()
    {
        var key = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .ApplySystemStyle("Оберіть що видалити:")
        .AddChoices(currentLog.Stats.Keys)
        .AddChoices("[bold]󰈆 Вихід[/]")
        );

        if (key != "[bold]󰈆 Вихід[/]")
        {
            bool confirm = AnsiConsole.Confirm("[bold]Підтвердіть видалення:[/]");

            if(confirm)
            {
                currentLog.Stats.Remove(key);
                settings.Weights.Remove(key);

                dataService.Save(currentLog);
                settingService.Save(settings);
            }
        }
    }
}
