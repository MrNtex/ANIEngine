using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInput: MonoBehaviour
{
    public int inputID;
    public InputType inputType;
    public Node myNode;
    private void Awake()
    {
        myNode = GetComponentInParent<Node>();
    }
}
