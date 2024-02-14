using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComparatorNode : Node
{
    private ComparationType comparationType;
    [SerializeField]
    private TMP_Dropdown comparationSelector;

    private bool latestValue;
    public override bool? Execute()
    {
        //Debug.Log($"Executing Logic Node {Inputs[0]}, {Inputs[1]}");
        switch (comparationType)
        {
            case ComparationType.Bigger:
                if ((float)Inputs[1].myNode.GetValue(Inputs[1].id) > (float)Inputs[2].myNode.GetValue(Inputs[2].id))
                {
                    return latestValue = true;
                }
                break;
            case ComparationType.Smaller:
                if ((float)Inputs[1].myNode.GetValue(Inputs[1].id) < (float)Inputs[2].myNode.GetValue(Inputs[2].id))
                {
                    return latestValue = true;
                }
                break;
            case ComparationType.Equal:
                if ((float)Inputs[1].myNode.GetValue(Inputs[1].id) == (float)Inputs[2].myNode.GetValue(Inputs[2].id))
                {
                    return latestValue = true;
                }
                break;
            case ComparationType.BiggerOrEqual:
                if ((float)Inputs[1].myNode.GetValue(Inputs[1].id) >= (float)Inputs[2].myNode.GetValue(Inputs[2].id))
                {
                    return latestValue = true;
                }
                break;
            case ComparationType.SmallerOrEqual:
                if ((float)Inputs[1].myNode.GetValue(Inputs[1].id) <= (float)Inputs[2].myNode.GetValue(Inputs[2].id))
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
        comparationType = (ComparationType)comparationSelector.value;
    }
}
enum ComparationType
{
    Bigger,
    Smaller,
    Equal,
    BiggerOrEqual,
    SmallerOrEqual
}

