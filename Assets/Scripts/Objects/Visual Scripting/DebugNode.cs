using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNode : Node
{
    public string Message;

    public override void Execute()
    {
        Debug.Log(Message + Inputs[0]);
    }
}
