using LifeTracker.Services;
using LifeTracker.Logic;
using LifeTracker.Utils;
using Microsoft.VisualBasic;


// Checking if directory exists
FileSystemConfig.EnsureDirectoriesCreated();

var userNotifyer = new UserNotifyer();

// Data services
var settingService = new SettingsService();
var dataService = new DataService(userNotifyer);
var logCollectingService = new LogCollectingService(dataService, settingService);

var levelUpSystem = new LevelUpSystem();
var logProvider = new DailyLogProvider(dataService, settingService);
var statsPresentationService = new StatsPresentationService(logCollectingService, settingService, levelUpSystem);

// loading log and settings files from system
var log = dataService.Load(DateTime.Now.ToString());
var setting = settingService.Load();

var inputService = new InputService();
var statsService = new StatsService(dataService, settingService, logProvider);

// loading controllers
var UiController = new UiController();
var controller = new AppController(inputService, statsService, UiController, statsPresentationService);

while(true)
{
    controller.MainMenu();
}
