using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicNode : Node
{
    private LogicType logicType;
    [SerializeField]
    private TMP_Dropdown logicSelector;

    private bool latestValue;
    public override bool? Execute()
    {
        Debug.Log($"Executing Logic Node {Inputs[0]}, {Inputs[1]}");
        switch(logicType)
        {
            case LogicType.And:
                if ((bool)Inputs[0].myNode.GetValue(Inputs[0].id) && (bool)Inputs[1].myNode.GetValue(Inputs[1].id))
                {
                    return latestValue = true;
                }
                break;
            case LogicType.Or:
                if ((bool)Inputs[0].myNode.GetValue(Inputs[0].id) || (bool)Inputs[1].myNode.GetValue(Inputs[1].id))
                {
                    return latestValue = true;
                }
                break;
            case LogicType.Xor:
                if ((bool)Inputs[0].myNode.GetValue(Inputs[0].id) ^ (bool)Inputs[1].myNode.GetValue(Inputs[1].id))
                {
                    return latestValue = true;
                }
                break;
        }
        return latestValue = false;
    }
    public override object GetValue(int id)
    {
        return latestValue;
    }
    public void SetLogic()
    {
        logicType = (LogicType)logicSelector.value;
    }
}
enum LogicType
{
    And,
    Or,
    Xor,
}
