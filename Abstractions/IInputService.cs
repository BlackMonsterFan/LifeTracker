namespace LifeTracker.Abstractions;

public interface IInputService
{
    T GetChoice<T>(string title) where T : struct, Enum; 
    int AskInt(string promt);
    (string, double) GetNewStatDetails();
    string GetStatName(IEnumerable<string> choices, string title);
    bool GetConfirm(string title);
}