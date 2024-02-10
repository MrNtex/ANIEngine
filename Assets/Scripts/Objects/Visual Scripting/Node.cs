using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public List<Node> Inputs = new List<Node>();
    public List<Node> Outputs = new List<Node>();

    public abstract void Execute();
    public abstract object GetValue();
}
