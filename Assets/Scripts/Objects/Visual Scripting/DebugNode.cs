using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugNode : Node
{
    public string Message;

    [SerializeField]
    private TMP_InputField messageInputField;

    public override void Execute()
    {
        Debug.Log(Message + " " + Inputs[1].GetValue());
    }
    public override object GetValue()
    {
        return null;
    }
    public void SetMessage()
    {
        Message = messageInputField.text;
    }
}
