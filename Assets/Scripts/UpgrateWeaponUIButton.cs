using UnityEngine.UI;
using UnityEngine;

public class UpgrateWeaponUIButton: MonoBehaviour, IButtonBehavior
{
    public int CostService { get; private set; }
    
    [SerializeField] private Button _upgrateWeaponButton;
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private DefaultGun _defaultGun;
    
    [SerializeField] private Image _buttonImage;
    
    private Color _notEnoughMoneyColor = new Color(4f / 255f, 70f / 255f, 0f, 78f / 255f);
    private Color _enoughMoneyColor = new Color(15f / 255f, 1f, 0f, 145f / 255f);

    private void Start()
    {
        CostService = 20;
        _upgrateWeaponButton.onClick.AddListener(OnButtonClick);
        UpdateButtonColor();
    }

    private void Update()
    {
        UpdateButtonColor();
    }

    private void OnDestroy()
    {
        _upgrateWeaponButton.onClick.RemoveListener(OnButtonClick);
    }
    
    public void OnButtonClick()
    {
        if (_moneyManager.MoneyCount >= CostService && _defaultGun.BulletPoints < 3 )
        {
            _defaultGun.BulletPoints++;
            _moneyManager.SpendMoney(CostService);
            UpdateButtonColor();
        }
        else
        {
            Debug.Log("Недостаточно денег для покупки помощника.");
        }
    }

    public void UpdateButtonColor()
    {
        if (_moneyManager.MoneyCount < CostService || _defaultGun.BulletPoints >= 3)
        {
            _buttonImage.color = _notEnoughMoneyColor;
        }
        else
        {
            _buttonImage.color = _enoughMoneyColor;
        }
    }
}