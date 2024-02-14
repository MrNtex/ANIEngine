using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public NodeOutput[] Inputs = new NodeOutput[5];
    public List<Node> Outputs = new List<Node>();
    public List<Node> FalseOutputs = new List<Node>();
    //public List<InputType> inputConnections = new List<InputType>();
    public List<NodeOutput> nodeOutputs = new List<NodeOutput>();

    public abstract bool? Execute(); // Null = node wasn't conditional, true = success, false = failure
    public abstract object GetValue(int id);

    public void UpdateLineRenderers()
    {
        /*for(int i = 0; i < Outputs.Length; i++)
        {
            if (Outputs[i] != null)
            {
                nodeOutputs[i].UpdateLineRenderers();
            }
        }
        for (int i = 0; i < FalseOutputs.Length; i++)
        {
            if (FalseOutputs[i] != null)
            {
                nodeOutputs[i].UpdateLineRenderers();
            }
        }*/
        for(int i = 0; i < nodeOutputs.Count; i++)
        {
            if (nodeOutputs[i] != null && nodeOutputs[i].connectionNodes.Count > 0)
                nodeOutputs[i].UpdateLineRenderers();
        }

        foreach (NodeOutput input in Inputs)
        {
            if (input != null)
            {
                input.UpdateLineRenderers();
            }

        }
    }
}
public enum InputType
{
    Entry,
    Value,
}
