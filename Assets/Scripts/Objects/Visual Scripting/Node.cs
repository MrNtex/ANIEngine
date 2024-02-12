using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public List<Node> Inputs = new List<Node>();
    public List<Node> Outputs = new List<Node>();

    public List<InputType> inputConnections = new List<InputType>();
    public List<InputType> outputConnections = new List<InputType>();

    public abstract void Execute();
    public abstract object GetValue();
}
public enum InputType
{
    Entry,
    Value,
}
