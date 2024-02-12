using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeOutput : MonoBehaviour
{
    public InputType inputType;
    public Node myNode;

    public LineRenderer lr;

    private Transform end;
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
    public void UpdateLineRenderers()
    {
        lr.SetPosition(0, (Vector2)transform.position);
        lr.SetPosition(1, (Vector2)end.position);
    }
    public void CreateConnection(Node target, Transform end)
    {
        myNode.Outputs.Add(target);
        target.Inputs.Add(myNode);
        lr.enabled = true;

        this.end = end;
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
