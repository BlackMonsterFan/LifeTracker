using System;
using Spectre.Console;
using LifeTracker;
using System.ComponentModel;
using Microsoft.VisualBasic;

var settingService = new SettingsService();
var dataService = new DataService();

// loading stats and settings files from system
var currentLog = dataService.Load();
var setting = settingService.Load();

// creating controller
var controller = new AppController(currentLog, setting, dataService, settingService);

bool isRunning = true;

while(isRunning)
{
    AnsiConsole.Clear();

    // ShowTable(currentData);

    var choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
    .Title("What do you wanna do?")
    .AddChoices("Add values", "Settings", "Exit")
    );

    controller.ChoiceHandler(choice);
}

static void ShowTable(DailyLog data)
{
    var statsTable = new Table().Border(TableBorder.Square).BorderColor(Color.Orange1);

    statsTable.AddColumn("[grey]Stat[/]");
    statsTable.AddColumn("[grey]Value[/]");

 
    AnsiConsole.Write(new Align(statsTable, HorizontalAlignment.Center));
}

// if (settingsChoice == "Add new stat")
//             {
//                 var key = AnsiConsole.Ask<string>("Name for the stat?");
//                 var weight = AnsiConsole.Ask<double>("How much XP it gives?");

//                 currentData.UpdateStat(key, 0);
//                 currentSetting.UpdateWeight(key, weight);
//                 SettingService.SaveSettings(currentSetting);
//             }
