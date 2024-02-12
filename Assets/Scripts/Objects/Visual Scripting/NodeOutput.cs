using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeOutput : MonoBehaviour
{
    public InputType inputType;
    public Node myNode;

    public LineRenderer lr;
    private void Start()
    {
        myNode = GetComponentInParent<Node>();
    }
    public void SetPositions(Vector2 pos, bool reset = false)
    {
        lr.SetPosition(0, (Vector2)transform.position);
        if(reset)
        {
            lr.SetPosition(1, (Vector2)transform.position);
            return;
        }
        lr.SetPosition(1, pos);
    }
    public void CreateConnection(Node target)
    {
        myNode.Outputs.Add(target);
        target.Inputs.Add(myNode);
        lr.enabled = true;
    }
    public void DestroyConnection()
    {
        foreach(Node node in myNode.Outputs)
        {
            node.Inputs.Remove(myNode);
        }
        myNode.Outputs.Clear();
        
    }
}
