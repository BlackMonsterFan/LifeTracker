using LifeTracker.Abstractions;

namespace LifeTracker;

public class AppController(IInputService inputService, IStatsService statsService, UiController ui, IStatsPresentationService statsPresentationService)
{
    public void MainMenu()
    {
        var statsInfo = statsPresentationService.GetStatsInfo();
        ui.ShowStatsTable(statsInfo);

        var choice = inputService.GetChoice<MenuOption>("Оберіть опцію:");
        ChoiceHandler(choice);
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
        var statsList = statsService.GetStatsList();
        var name = inputService.GetStatName(statsList, "Зробити запис в:");

        if (name != "Вихід")
        {
            var weight = statsService.GetWeight(name);
            string prompt = $"[bold orange1] Введіть кількість одиниць: [/] \n [dim] 1 = {weight}Xp: [/]";

            var amount = inputService.AskInt(prompt);

            statsService.AddProgress(name, amount);
        }

        return;
    }

    public void DeleteValueMenu()
    {
        var statsList = statsService.GetStatsList();
        var name = inputService.GetStatName(statsList, "Видалити з:");

        if (name != "Вихід")
        {
            var weight = statsService.GetWeight(name);
            string prompt = $"[bold orange1] Введіть кількість одиниць: [/] \n [dim] 1 = {weight}Xp: [/]";

            var amount = inputService.AskInt(prompt);

            statsService.AddProgress(name, -amount);
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
        var statsList = statsService.GetStatsList();

        string name = inputService.GetStatName(statsList, "Оберіть що відредагувати:");

        if (name != "Вихід")
        {   
            var (newName, weight) = inputService.GetNewStatDetails();

            statsService.RenameStat(name, newName);
            statsService.UpdateWeight(newName, weight);
        }
    }

    public void DeleteStat()
    {
        var statsList = statsService.GetStatsList();

        string name = inputService.GetStatName(statsList, "Оберіть що видалити:");

        if (name != "Вихід")
        {
            bool confirm = inputService.GetConfirm("Підтвердіть видалення:");

            if(confirm)
            {
                statsService.RemoveStat(name);
            }
        }
    }
}
