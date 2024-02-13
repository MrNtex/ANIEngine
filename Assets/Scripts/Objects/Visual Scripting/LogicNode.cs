using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicNode : Node
{
    private LogicType logicType;
    public override bool? Execute()
    {
        switch(logicType)
        {
            case LogicType.And:
                if ((Inputs[0] || Inputs[0] == null) && (Inputs[1] || Inputs[1] == null))
                {
                    return true;
                }
                break;
            case LogicType.Or:
                if ((Inputs[0] || Inputs[0] == null) || (Inputs[1] || Inputs[1] == null))
                {
                    return true;
                }
                break;
            case LogicType.Xor:
                if ((Inputs[0] || Inputs[0] == null) ^ (Inputs[1] || Inputs[1] == null))
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
}
enum LogicType
{
    And,
    Or,
    Xor,
}
