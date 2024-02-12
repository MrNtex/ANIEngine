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
        if (Inputs[1] == null)
        {
            Debug.Log(Message);
            return;
        }
        if (Inputs[1].GetValue() != null)
        {
            Debug.Log(Message + " " + Inputs[1].GetValue());
        }
        else
        {
            Debug.Log(Message);
        }
        
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
