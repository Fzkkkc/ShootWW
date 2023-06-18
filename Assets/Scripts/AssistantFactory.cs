using UnityEngine;
using System.Collections.Generic;

public class AssistantFactory : MonoBehaviour
{
    public List<GameObject> AssistantsList;
    
    public void ActivateAssistant()
    {
        if (AssistantsList.Count == 0)
        {
            Debug.Log("Список объектов пуст.");
            return;
        }

        int randomIndex = Random.Range(0, AssistantsList.Count);
        GameObject randomObject = AssistantsList[randomIndex];
        randomObject.SetActive(true);
        
        AssistantsList.RemoveAt(randomIndex);
    }
}