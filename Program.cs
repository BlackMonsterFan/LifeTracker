using System;
using Spectre.Console;
using LifeTracker;

var settingService = new SettingsService();
var dataService = new DataService();

// loading stats and settings files from system
var currentLog = dataService.Load();
var setting = settingService.Load();

// loading controllers
var UiController = new UiController();
var controller = new AppController(currentLog, setting, dataService, settingService, UiController);

while(true)
{
    AnsiConsole.Clear();
    
    var choice = controller.ShowMainMenu();

    controller.ChoiceHandler(choice);

    

}
