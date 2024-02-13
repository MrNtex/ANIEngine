using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeOutput : MonoBehaviour
{
    public InputType inputType;
    public Node myNode;
    public bool isNotConnection;

    public LineRenderer lr;

    private Transform end;

    public int id;
    public List<Node> connectionNodes = new List<Node>();
    public List<int> connectionsIds = new List<int>();
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
    public void CreateConnection(Node target, Transform end, int sourceId, int targetId, bool notConnection)
    {
        
        if (notConnection)
        {
            myNode.FalseOutputs[sourceId] = target;
        }
        else
        {
            myNode.Outputs[sourceId] = target;
        }
        target.Inputs[targetId] = myNode;
        lr.enabled = true;

        this.end = end;

        connectionNodes.Add(target);
        connectionsIds.Add(targetId);
    }
    public void DestroyConnections()
    {
        myNode.Outputs[id] = null;
        foreach (Node cNode in connectionNodes) // It clears all connections from connected nodes
        {
            foreach(int cId in connectionsIds)
            {
                cNode.Inputs[cId] = null;
            }
        }
        connectionNodes.Clear();
        connectionsIds.Clear();
    }
}
