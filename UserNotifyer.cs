using LifeTracker.Abstractions;
using Spectre.Console;

public class UserNotifyer : IUserNotifyer
{
    public void Error(string message){
        AnsiConsole.MarkupLineInterpolated($"[bold red]✗ Error:[/] Error: '{message}'");
    }
}