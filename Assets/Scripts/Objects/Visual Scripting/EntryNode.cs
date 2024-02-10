using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryNode : Node
{
    override public void Execute()
    {
        Debug.Log("EntryNode!");
    }
    override public object GetValue()
    {
        return 0;
        // Do nothing
    }
}
