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
                .AddChoices("Add new stats", "Edit stats", "Delete stats", "Exit")
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
            case "Add new stats":
                AddNewStat();
                break;

            case "Edit stats":
                EditStat();
                break;

            case "Exit":
                AnsiConsole.Clear();
                break;
        }
    }

    public void AddNewStat()
    {
        string key = AnsiConsole.Ask<string>("Name for the stat?");
        double weight = AnsiConsole.Ask<double>("How much XP it gives?");

        currentLog.UpdateStat(key, 0);
        settings.UpdateWeight(key, weight);
        dataService.Save(currentLog);
        settingService.Save(settings);
    }

    public void EditStat() 
    {
        var key = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Choose stat to edit:")
        .AddChoices(currentLog.Stats.Keys)
        .AddChoices("Exit")
        );

        if (key != "Exit")
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose stat to edit:")
            .AddChoices("Edit name", "Edit XP value", "Exit")
            );

            switch (choice)
            {
                case "Edit name":
                    string newName = AnsiConsole.Ask<string>($"Please enter new name: (old one is {key})");
                    bool confirmName = AnsiConsole.Confirm("Are you sure?");

                    if (confirmName)
                    {
                        currentLog.Stats.Remove(key, out double quantity);
                        currentLog.UpdateStat(newName, quantity);
                        dataService.Save(currentLog);

                    }else break; 
                    break;
                    
                case "Edit XP value":
                    double newValue = AnsiConsole.Ask<double>($"Please enter new value: (old one is {settings.Weights[key]})");
                    bool confirmValue = AnsiConsole.Confirm("Are you sure?");

                    if (confirmValue)
                    {
                        settings.UpdateWeight(key, newValue);
                        settingService.Save(settings);

                    }else break; 
                    break;

                case "Exit":
                    break;
            }
        }

        // currentLog.UpdateStat(key, 0);
        // settings.UpdateWeight(key, weight);
        // dataService.Save(currentLog);
        // settingService.Save(settings);
    }
}