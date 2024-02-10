using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScripting : MonoBehaviour
{
    public Node updateNode;
    
    void Start()
    {
        Queue<Node> nodesToProcess = new Queue<Node>();
        nodesToProcess.Enqueue(updateNode);

        while (nodesToProcess.Count > 0)
        {
            Node currentNode = nodesToProcess.Dequeue();
            currentNode.Execute();

            foreach (Node output in currentNode.Outputs)
            {
                nodesToProcess.Enqueue(output);
            }
        }
    }
}
