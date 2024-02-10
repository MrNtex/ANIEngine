using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OperationNode : Node
{
    public OperationType operationType;
    public TMP_Dropdown operationDropdown;
    public float Input1 { get; set; }
    public float Input2 { get; set; }
    public float Output { get; private set; }

    public override void Execute()
    {
        switch (operationType)
        {
            case OperationType.Add:
                Output = Input1 + Input2;
                break;
            case OperationType.Subtract:
                Output = Input1 - Input2;
                break;
            case OperationType.Multiply:
                Output = Input1 * Input2;
                break;
            case OperationType.Divide:
                Output = Input1 / Input2;
                break;
        }

        foreach (Node output in Outputs)
        {
            output.Inputs.Remove(this);
            output.Inputs.Add(this);

            output.Execute();
        }
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