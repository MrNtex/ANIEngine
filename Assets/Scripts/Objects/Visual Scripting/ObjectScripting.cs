using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScripting : MonoBehaviour
{
    public Node updateNode, startNode;
    public EntryNode entryNode;

    public GameObject script;
    public int scriptID;
    private void Awake()
    {
        entryNode = updateNode as EntryNode;
    }
    private void OnEnable()
    {
        PlayMode.OnPlayStateChanged += HandlePlayStateChanged;
    }

    private void OnDisable()
    {
        PlayMode.OnPlayStateChanged -= HandlePlayStateChanged;
    }
    private void HandlePlayStateChanged(bool isPlaying)
    {
        if (startNode != null && isPlaying)
        {
            Debug.Log("Game resumed");
            PerformLogic(startNode);
        }
    }
    void Update()
    {
        if (updateNode != null && Time.timeScale != 0)
        {
            PerformLogic(updateNode);
        }
        
    }

    private void PerformLogic(Node begining)
    {
        Queue<Node> nodesToProcess = new Queue<Node>();
        nodesToProcess.Enqueue(begining);

        while (nodesToProcess.Count > 0)
        {
            Node currentNode = nodesToProcess.Dequeue();
            bool? condition = currentNode.Execute();
            if(condition == null || condition == true)
            {
                foreach (Node output in currentNode.Outputs)
                {
                    if (output != null)
                        nodesToProcess.Enqueue(output);
                }
            }
            else
            {
                foreach (Node output in currentNode.FalseOutputs)
                {
                    if (output != null)
                        nodesToProcess.Enqueue(output);
                }
            }
        }
    }
}
