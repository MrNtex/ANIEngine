using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyBoardInputNode : Node
{
    [SerializeField]
    private TMP_InputField keyCodeInputField;
    [SerializeField]
    private TMP_Dropdown inputTypeDropdown;

    private KeyBoardInputType keyBoardInputType;
    private KeyCode keyCode;

    private bool latestValue;
    public override bool? Execute()
    {
        switch (keyBoardInputType)
        {
            case KeyBoardInputType.KeyDown:
                return latestValue = Input.GetKeyDown(keyCode);
            case KeyBoardInputType.KeyPress:
                return latestValue = Input.GetKey(keyCode);
            case KeyBoardInputType.KeyUp:
                return latestValue = Input.GetKeyUp(keyCode);
        }
        return latestValue = false;
    }
    public override object GetValue(int id)
    {
        return latestValue;
    }

    public void SetKeyCode()
    {
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyCodeInputField.text);
    }
    public void SetInputType()
    {
        keyBoardInputType = (KeyBoardInputType)inputTypeDropdown.value;
    }
}
public enum KeyBoardInputType
{
    KeyDown,
    KeyPress,
    KeyUp
}
