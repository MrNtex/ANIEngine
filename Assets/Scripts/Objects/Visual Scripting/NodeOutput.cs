using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeOutput : MonoBehaviour
{
    int id;
    public Node myNode;

    public LineRenderer lr;

    public void SetPositions(Vector2 pos)
    {
        lr.SetPosition(0, (Vector2)transform.position);
        lr.SetPosition(1, pos);
    }
    public void CreateConnection(Node target)
    {
        myNode.Outputs.Add(target);
        target.Inputs.Add(myNode);
        lr.enabled = true;
    }
}
