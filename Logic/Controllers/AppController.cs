using LifeTracker.Abstractions;
using LifeTracker.Services;

namespace LifeTracker.Logic;

public class AppController(IInputService inputService, IStatsService statsService, UiController ui, IStatsPresentationService statsPresentationService)
{
    public void MainMenu()
    {
        var statsInfo = statsPresentationService.GetStatsInfo();
        ui.ShowStatsTable(statsInfo);

        var choice = inputService.GetChoice<MenuOption>("Оберіть опцію:");
        ChoiceHandler(choice);

        ui.Clear();
    }

    public void ChoiceHandler(MenuOption choice)
    {
        switch (choice)
        {
            case MenuOption.Add:
                AddStatValue();
                break;

            case MenuOption.Delete:
                DeleteValueMenu();
                break;

            case MenuOption.Settings:
                var settingChoice = inputService.GetChoice<SettingOption>("Оберіть що налаштувати:");
                SettingHandler(settingChoice);
                break;
        
                case MenuOption.Exit:
                Environment.Exit(0);
                break;
        }
    }

    public void AddStatValue()
    {
        var statsDefinitions = statsService.GetStatsList();
        var statsList = statsDefinitions.Select(statDef => statDef.Name);

        var name = inputService.GetStatName(statsList, "Зробити запис в:");

        if (name != "Вихід")
        {
            var id = statsService.GetId(name);
            var weight = statsService.GetWeight(id);

            string prompt = $"[bold orange1] Введіть кількість одиниць: [/] \n [dim] 1 = {weight}Xp: [/]";

            var amount = inputService.AskInt(prompt);

            statsService.AddProgress(id, amount);
        }

        return;
    }

    public void DeleteValueMenu()
    {
        var statsDefinitions = statsService.GetStatsList();
        var statsList = statsDefinitions.Select(statDef => statDef.Name);

        var name = inputService.GetStatName(statsList, "Видалити з:");

        if (name != "Вихід")
        {
            var id = statsService.GetId(name);
            var weight = statsService.GetWeight(id);

            string prompt = $"[bold orange1] Введіть кількість одиниць: [/] \n [dim] 1 = {weight}Xp: [/]";

            var amount = inputService.AskInt(prompt);

            statsService.AddProgress(id, -amount);
        }

        return;
    }

    public void SettingHandler(SettingOption choice)
    {
        switch (choice)
        {
            case SettingOption.Add:
                NewStat();
                break;

            case SettingOption.Edit:
                EditStat();
                break;

            case SettingOption.Delete:
                DeleteStat();
                break;

            case SettingOption.Exit:
                break;
        }
    }

    public void NewStat()
    {
        var (name, weight) = inputService.GetNewStatDetails();

        statsService.AddNewStat(name, weight);
    }

    public void EditStat() 
    {
        var statsDefinitions = statsService.GetStatsList();
        var statsList = statsDefinitions.Select(statDef => statDef.Name);

        string name = inputService.GetStatName(statsList, "Оберіть що відредагувати:");

        if (name != "Вихід")
        {   
            var id = statsService.GetId(name);
            var (newName, weight) = inputService.GetNewStatDetails();

            statsService.RenameStat(id, newName);
            statsService.UpdateWeight(id, weight);
        }
    }

    public void DeleteStat()
    {
        var statsDefinitions = statsService.GetStatsList();
        var statsList = statsDefinitions.Select(statDef => statDef.Name);

        string name = inputService.GetStatName(statsList, "Оберіть що видалити:");

        if (name != "Вихід")
        {
            bool confirm = inputService.GetConfirm("Підтвердіть видалення:");

            if(confirm)
            {
                var id = statsService.GetId(name);
                statsService.RemoveStat(id);
            }
        }
    }
}
