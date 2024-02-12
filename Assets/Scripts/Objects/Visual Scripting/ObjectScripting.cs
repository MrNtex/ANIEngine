using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScripting : MonoBehaviour
{
    public Node updateNode;
    public EntryNode entryNode;
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
        if (isPlaying && entryNode.entryType == EntryType.Start)
        {
            Debug.Log("Game resumed");
            PerformLogic();
        }
    }
    void Update()
    {
        if (entryNode.entryType != EntryType.Update || Time.timeScale == 0)
        {
            return;
        }
        PerformLogic();
    }

    private void PerformLogic()
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
