using UnityEngine;
using TMPro;
using System;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshProUGUI _subText;
    private Action _buttonAction;
    
    public void SetWinPanel()
    {
        _panel.SetActive(true);
        _mainText.text = "YOU WIN!";
        _subText.text = "Next Level";
        _buttonAction = () => Debug.Log("Next levele");
    }

    public void SetLosePanel()
    {
        _panel.SetActive(true);
        _mainText.text = "YOU LOSE";
        _subText.text = "Restart";
        _buttonAction = () => Debug.Log("Restart");
    }
    
    public void OnButtonClick()
    {
        _buttonAction?.Invoke();
    }
}