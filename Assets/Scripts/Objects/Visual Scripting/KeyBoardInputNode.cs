using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyBoardInputNode : Node
{
    public float Value;
    [SerializeField]
    private TMP_InputField valueInputField;

    public override bool? Execute()
    {
        return null;
    }
    public override object GetValue()
    {
        return Value;
    }

    public void SetValue()
    {
        Value = float.Parse(valueInputField.text);
    }
}
