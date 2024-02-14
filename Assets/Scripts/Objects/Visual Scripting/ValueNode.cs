using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueNode : Node
{
    public float Value;
    [SerializeField]
    private TMP_InputField valueInputField;

    public override bool? Execute()
    {
        if (Inputs[1] != null)
        {
            Value = (float)Inputs[1].myNode.GetValue(Inputs[1].id);
        }
        return null;
    }
    public override object GetValue(int id)
    {
        return Value;
    }

    public void SetValue()
    {
        Value = float.Parse(valueInputField.text);
    }
}
