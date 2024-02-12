using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueNode : Node
{
    public float Value;
    [SerializeField]
    private TMP_InputField valueInputField;

    public override void Execute()
    {
        return;
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
