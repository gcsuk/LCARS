namespace LCARS.Data;

public record AlertState
{
    public string State { get; set; } = "";
    public event Action OnChange;

    public void SetAlertState(string state)
    {
        State = $"condition-{state}";
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}