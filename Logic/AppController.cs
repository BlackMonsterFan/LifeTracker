namespace LifeTracker;
using Spectre.Console;

public class AppController(DailyLog currentLog, UserSettings settings, DataService dataService, SettingsService settingService)
{
    private DailyLog _currentLog = currentLog;
    private UserSettings _settings = settings;
    private DataService _dataService = dataService;
    private SettingsService _settingsService = settingService;

    public void ChoiceHandler(string choice)
    {
        switch (choice)
        {
            case "Settings":
                var settingsChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What do you wanna set up?")
                .AddChoices("Add new stat", "Edit stat", "Delete stat", "Exit")
                );

                SettingHandler(settingsChoice);

                break;
        
                case "Exit":
                Environment.Exit(0);
                AnsiConsole.Clear();
                break;
        }
    }

    public void SettingHandler(string choice)
    {
        switch (choice)
        {
            case "Add new stat":
                AddNewStat();
                break;

            case "Exit":
                AnsiConsole.Clear();
                break;
        }
    }

    public void AddNewStat()
    {
        var key = AnsiConsole.Ask<string>("Name for the stat?");
        var weight = AnsiConsole.Ask<double>("How much XP it gives?");

        currentLog.UpdateStat(key, 0);
        settings.UpdateWeight(key, weight);
        dataService.Save(currentLog);
        settingService.Save(settings);
    }
}