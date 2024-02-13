using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public Node[] Inputs = new Node[5];
    public Node[] Outputs = new Node[5];
    public Node[] FalseOutputs = new Node[5];
    //public List<InputType> inputConnections = new List<InputType>();
    public List<NodeOutput> nodeOutputs = new List<NodeOutput>();

    public abstract bool? Execute(); // Null = node wasn't conditional, true = success, false = failure
    public abstract object GetValue();

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

        foreach (Node input in Inputs)
        {
            if(input != null)
            {
                foreach (NodeOutput output in input.nodeOutputs)
                {
                    if (output != null)
                    {
                        output.UpdateLineRenderers();
                    }
                    
                }
            }
            
        }
    }
}
public enum InputType
{
    Entry,
    Value,
}
