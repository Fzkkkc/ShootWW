using UnityEngine;
using UnityEngine.UI;

public class AssistantPlayerUIButton : MonoBehaviour, IButtonBehavior
{
    public Button AssistantPlayerButton;
    public AssistantFactory AssistantFactory;
    public MoneyManager MoneyManager;
    
    public int CostService { get; private set; }

    private Color _notEnoughMoneyColor = new Color(4f / 255f, 70f / 255f, 0f, 78f / 255f);
    private Color _enoughMoneyColor = new Color(15f / 255f, 1f, 0f, 145f / 255f);
    
    [SerializeField] private Image _buttonImage;
    
    private void Start()
    {
        CostService = 10;
        AssistantPlayerButton.onClick.AddListener(OnButtonClick);
        UpdateButtonColor();
    }

    private void Update()
    {
        UpdateButtonColor();
    }

    private void OnDestroy()
    {
        AssistantPlayerButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (MoneyManager.MoneyCount >= CostService && AssistantFactory.AssistantsList.Count != 0)
        {
            AssistantFactory.ActivateAssistant();
            MoneyManager.SpendMoney(CostService);
            UpdateButtonColor();
        }
        else
        {
            Debug.Log("Недостаточно денег для покупки помощника.");
        }
    }
    
    public void UpdateButtonColor()
    {
        if (MoneyManager.MoneyCount < CostService || AssistantFactory.AssistantsList.Count == 0)
        {
            _buttonImage.color = _notEnoughMoneyColor;
        }
        else
        {
            _buttonImage.color = _enoughMoneyColor;
        }
    }
}