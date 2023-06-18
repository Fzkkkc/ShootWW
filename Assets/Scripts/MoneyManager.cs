using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [HideInInspector] public int MoneyCount = 0;
    
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private EnemyManager _enemyManager;
    
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        MoneyCount = PlayerPrefs.GetInt("Money", 0);
        UpdateMoneyText();
        _enemyManager.OnEnemyKilledEvent += OnOnEnemyKilledEvent;
    }

    private void OnOnEnemyKilledEvent(IEnemyBehavior enemy)
    {
        AddMoney(enemy.Reward);
    }
    
    public void AddMoney(int amount)
    {
        MoneyCount += amount;
        UpdateMoneyText();
        SaveMoney();
    }

    public void SpendMoney(int amount)
    {
        MoneyCount -= amount;
        UpdateMoneyText();
        SaveMoney();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = MoneyCount.ToString();
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", MoneyCount);
        PlayerPrefs.Save();
    }
}