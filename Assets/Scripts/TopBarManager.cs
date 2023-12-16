using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TopBarManager : MonoBehaviour
{
    public CustomButton[] buttons;
    public GameObject buttonPrefab;
    public Transform buttonsParent;

    void Start()
    {
        foreach (var customButton in buttons)
        {
            CreateButton(customButton);
        }
    }
    void CreateButton(CustomButton customButton)
    {
        GameObject newButtonObj = Instantiate(buttonPrefab, buttonsParent);
        Button newButton = newButtonObj.GetComponent<Button>();
        newButtonObj.GetComponentInChildren<TMP_Text>().text = customButton.text;
        if(customButton.action != null) newButton.onClick.AddListener(customButton.action.Invoke);

        // Adjust size and position here if needed
    }
}
[Serializable]
public struct CustomButton
{
    public string text;
    public UnityEvent action;  // Assuming UnityEvent for simplicity
    // Add other properties as needed
}
