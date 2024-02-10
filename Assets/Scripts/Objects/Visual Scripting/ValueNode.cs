using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueNode : Node
{
    public float Value;

    public override void Execute()
    {
        return;
    }
    public override object GetValue()
    {
        return Value;
    }
}
