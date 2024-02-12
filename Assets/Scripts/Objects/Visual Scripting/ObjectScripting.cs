using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScripting : MonoBehaviour
{
    public Node updateNode, startNode;
    public EntryNode entryNode;

    public GameObject script;
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
        if (entryNode != null && isPlaying && entryNode.entryType == EntryType.Start)
        {
            Debug.Log("Game resumed");
            PerformLogic(startNode);
        }
    }
    void Update()
    {
        if (entryNode != null && entryNode.entryType != EntryType.Update || Time.timeScale == 0)
        {
            return;
        }
        PerformLogic(updateNode);
    }

    private void PerformLogic(Node begining)
    {
        Queue<Node> nodesToProcess = new Queue<Node>();
        nodesToProcess.Enqueue(begining);

        while (nodesToProcess.Count > 0)
        {
            Node currentNode = nodesToProcess.Dequeue();
            currentNode.Execute();

            foreach (Node output in currentNode.Outputs)
            {
                if(output != null)
                nodesToProcess.Enqueue(output);
            }
        }
    }
}
