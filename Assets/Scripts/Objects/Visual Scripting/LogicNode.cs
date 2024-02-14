using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicNode : Node
{
    private LogicType logicType;
    [SerializeField]
    private TMP_Dropdown logicSelector;
    public override bool? Execute()
    {
        Debug.Log($"Executing Logic Node {Inputs[0]}, {Inputs[1]}");
        switch(logicType)
        {
            case LogicType.And:
                if ((bool)Inputs[0].GetValue() && (bool)Inputs[1].GetValue())
                {
                    return true;
                }
                break;
            case LogicType.Or:
                if ((bool)Inputs[0].GetValue() || (bool)Inputs[1].GetValue())
                {
                    return true;
                }
                break;
            case LogicType.Xor:
                if ((bool)Inputs[0].GetValue() ^ (bool)Inputs[1].GetValue())
                {
                    return true;
                }
                break;
        }
        return false;
    }
    public override object GetValue()
    {
        return null;
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
