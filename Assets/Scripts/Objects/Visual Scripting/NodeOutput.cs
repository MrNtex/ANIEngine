using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class NodeOutput : MonoBehaviour
{
    public InputType inputType;
    public Node myNode;
    public bool isNotConnection;

    public LineRenderer lr;
    private Dictionary<LineRenderer, NodeInput> connectionLines = new Dictionary<LineRenderer, NodeInput>();

    private Transform end;

    public int id;
    public List<Node> connectionNodes = new List<Node>();
    public List<int> connectionsIds = new List<int>();
    private void Start()
    {
        myNode = GetComponentInParent<Node>();
    }
    public LineRenderer CreateANewLine()
    {
        GameObject line = new GameObject("Line");
        line.transform.SetParent(transform);
        LineRenderer newlr = line.AddComponent<LineRenderer>();
        newlr.positionCount = 2;
        newlr.startWidth = lr.startWidth;
        newlr.endWidth = lr.endWidth;
        newlr.material = lr.material;
        newlr.startColor = lr.startColor;
        newlr.endColor = lr.endColor;
        newlr.enabled = true;
        newlr.sortingOrder = 3;
        lr.SetPosition(0, (Vector2)transform.position);
        lr.SetPosition(1, (Vector2)transform.position);

        
        return newlr;
    }
    public void SetPositions(Vector2 pos, LineRenderer myLR, bool reset = false)
    {
        myLR.SetPosition(0, (Vector2)transform.position);
        if(reset)
        {
            myLR.SetPosition(1, (Vector2)transform.position);
            return;
        }
        myLR.SetPosition(1, pos);
    }
    public void UpdateLineRenderers()
    {
        foreach(LineRenderer lr in connectionLines.Keys)
        {
            lr.SetPosition(0, (Vector2)transform.position);
            lr.SetPosition(1, (Vector2)connectionLines[lr].transform.position);
        }
    }
    public void CreateConnection(NodeInput target, Transform end, int sourceId, int targetId, bool notConnection, LineRenderer myLR)
    {
        
        if (notConnection)
        {
            myNode.FalseOutputs.Add(target.myNode);
        }
        else
        {
            myNode.Outputs.Add(target.myNode);
        }
        target.myNode.Inputs[targetId] = this;
        connectionLines.Add(myLR, target);

        this.end = end;

        connectionNodes.Add(target.myNode);
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
            myNode.Outputs.Remove(cNode);
        }
        foreach (LineRenderer lr in connectionLines.Keys)
        {
            Destroy(lr.gameObject);
        }
        connectionLines.Clear();
        connectionNodes.Clear();
        connectionsIds.Clear();
    }
    public void DestroyLineRender(LineRenderer connection)
    {
        connectionLines.Remove(connection);
        Destroy(connection.gameObject);
    }
}
