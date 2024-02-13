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
    public override bool? Execute()
    {
        switch (keyBoardInputType)
        {
            case KeyBoardInputType.KeyDown:
                return Input.GetKeyDown(keyCode);
            case KeyBoardInputType.KeyPress:
                return Input.GetKey(keyCode);
            case KeyBoardInputType.KeyUp:
                return Input.GetKeyUp(keyCode);
        }
        return false;
    }
    public override object GetValue()
    {
        return null;
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
