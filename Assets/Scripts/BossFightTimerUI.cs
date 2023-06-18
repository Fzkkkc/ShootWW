using TMPro;
using UnityEngine;

public class BossFightTimerUI : MonoBehaviour
{
    public TextMeshProUGUI _timerText;
    public BossSpawn BossSpawn;
    
    private void Update()
    {
        UpdateTimer();
    }
    
    private void UpdateTimer()
    {
        float timer = BossSpawn.BossSpawnTimer;
        if (timer <= 0f)
        {
            _timerText.text = "BOSS FIGHT!!!";
        }
        else
        {
            string countdownText = timer.ToString("0");
            _timerText.text = "BOSS FIGHT IN " + countdownText;
        }
    }
}