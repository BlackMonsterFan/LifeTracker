using System.ComponentModel;

namespace LifeTracker.Abstractions;

public enum SettingOption
{
    [Description("Створити нову статистику")]
    Add,
    [Description("Видалити статистику")]
    Delete,
    [Description("Відредагувати статистику")]
    Edit,
    [Description("[bold]🚪 Вихід[/]")]
    Exit
}