public interface IButtonBehavior
{
    void OnButtonClick();
    void UpdateButtonColor();
    int CostService { get; }
}