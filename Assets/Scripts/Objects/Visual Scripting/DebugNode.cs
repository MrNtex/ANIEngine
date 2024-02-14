using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugNode : Node
{
    public string Message;

    [SerializeField]
    private TMP_InputField messageInputField;

    public override bool? Execute()
    {
        if (Inputs[1] == null)
        {
            Debug.Log(Message);
            return null;
        }
        if (Inputs[1].myNode.GetValue(Inputs[1].id) != null)
        {
            Debug.Log(Message + " " + Inputs[1].myNode.GetValue(Inputs[1].id));
        }
        else
        {
            Debug.Log(Message);
        }
        return null;
    }
    public override object GetValue(int id)
    {
        return null;
    }
    public void SetMessage()
    {
        Message = messageInputField.text;
    }
}
