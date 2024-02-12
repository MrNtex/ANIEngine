using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OperationNode : Node
{
    public OperationType operationType;
    public TMP_Dropdown operationDropdown;
    public float Output { get; private set; }

    public override object GetValue()
    {
        object Input1 = Inputs[0].GetValue();
        object Input2 = Inputs[1].GetValue();
        switch (operationType)
        {
            case OperationType.Add:
                Output = (float)Input1 + (float)Input2;
                break;
            case OperationType.Subtract:
                Output = (float)Input1 - (float)Input2;
                break;
            case OperationType.Multiply:
                Output = (float)Input1 * (float)Input2;
                break;
            case OperationType.Divide:
                Output = (float)Input1 / (float)Input2;
                break;
        }

        return Output;
    }
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void SetOperation()
    {
        operationType = (OperationType)operationDropdown.value;
    }
}
public enum OperationType
{
    Add,
    Subtract,
    Multiply,
    Divide
}