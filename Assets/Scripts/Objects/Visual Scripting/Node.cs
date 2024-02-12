using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public List<Node> Inputs = new List<Node>();
    public List<Node> Outputs = new List<Node>();

    //public List<InputType> inputConnections = new List<InputType>();
    public List<NodeOutput> nodeOutputs = new List<NodeOutput>();

    public abstract void Execute();
    public abstract object GetValue();

    public void UpdateLineRenderers()
    {
        if(Outputs.Count != 0)
        {
            foreach (NodeOutput output in nodeOutputs)
            {
                output.UpdateLineRenderers();
            }
        }
        
        foreach(Node input in Inputs)
        {
            foreach(NodeOutput output in input.nodeOutputs)
            {
                output.UpdateLineRenderers();
            }
        }
    }
}
public enum InputType
{
    Entry,
    Value,
}
