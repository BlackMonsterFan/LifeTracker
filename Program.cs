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

// loading controllers
var controller = new AppController(currentLog, setting, dataService, settingService);
var ui = new UiController();


while(true)
{
    AnsiConsole.Clear();

    // ShowTable(currentData);
    ui.ShowStatsTable(currentLog);

    var choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
    .Title("What do you wanna do?")
    .AddChoices("Add values", "Settings", "Exit")
    );

    controller.ChoiceHandler(choice);
}
