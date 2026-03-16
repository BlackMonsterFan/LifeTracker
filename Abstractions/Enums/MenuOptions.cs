using System.ComponentModel;

namespace LifeTracker.Abstractions;

public enum MenuOption
{
    [Description("Записати значення")]
    Add,
    [Description("Видалити значення")]
    Delete,
    [Description("Налаштування")]
    Settings,
    [Description("Вихід")]
    Exit
}