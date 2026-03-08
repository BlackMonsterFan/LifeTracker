using System;
using Spectre.Console;
using LifeTracker;

// Loading stats file from system
var currentData = DataService.Load();

bool isRunning = true;

while(isRunning)
{
    AnsiConsole.Clear();

    ShowTable(currentData);

    var choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
    .Title("What do you wanna do?")
    .AddChoices("Add study hours", "Add Monster", "Add carbs", "Gym session", "Exit")
    );

    switch (choice)
    {
        case "Add study hours":
            int hoursToAdd = AnsiConsole.Ask<int>("So how much do you studied?");
            currentData.AddStudyTime(hoursToAdd);
            break;

        case "Add Monster":
            var MonsterCount = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("You sure that you wanna add another can for today?")
                .AddChoices("Yes", "No")
                );

            if (MonsterCount == "Yes")
            {
                currentData.AddMonster();
            }
            break;

        case "Add carbs":
            int carbsToAdd = AnsiConsole.Ask<int>("So how much carbs you eaten?");
            currentData.AddCarbs(carbsToAdd);
            break;

        case "Gym session":
            var isGymVisited = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("So you visited dym today?")
                .AddChoices("Yes", "No, thats why i stall small.")
                );

            if (isGymVisited == "Yes")
            {
                currentData.SetGymStatus(true);
            }
            else
            {
                currentData.SetGymStatus(false);
            }
            break;
        
        case "Exit":
            isRunning = false;
            AnsiConsole.Clear();
            break;
    }

    DataService.Save(currentData);
}

static void ShowTable(DailyLog data)
{
    var statsTable = new Table().Border(TableBorder.Square).BorderColor(Color.Orange1);

    statsTable.AddColumn("[grey]Stat[/]");
    statsTable.AddColumn("[grey]Value[/]");

    statsTable.AddRow("Intelligence", $"[cyan]{data.IntelligentXP} XP[/]");
    statsTable.AddRow("Carbohydrates", $"[green]{data.CarbsCount} / 500g[/]");
    statsTable.AddRow("Monsters", $"[bold red]{data.MonsterCount} CANS[/]");
    statsTable.AddRow("Gym", $"[bold red]{data.GymVisited}[/]");

    AnsiConsole.Write(new Align(statsTable, HorizontalAlignment.Center));
}

