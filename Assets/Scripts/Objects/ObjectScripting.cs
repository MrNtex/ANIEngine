using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ObjectScripting : MonoBehaviour
{
    [SerializeField]
    [Header("0 - Update, \n1 - Start, \n2 - OnEnable")]
    private bool[] entry = new bool[3];

    //private List<Block> blocks = new List<Block>();
    List<Block> blocks = new List<Block>();

    List<Block> startConnections = new List<Block>();
    List<Block> updateConnections = new List<Block>();
    List<Block> onEnableConnections = new List<Block>();
    // Start is called before the first frame update
    void Start()
    {
        if (entry[0])
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(entry[1]) 
        {
            
        }
    }
    private void OnEnable()
    {
        if (entry[2])
        {

        }
    }
    public void AddBlock(Type type)
    {
        Block block = new Block();
        block.type = type;
        blocks.Add(block);
    }
    public void BlockAction(Block block) // Input 0 is reserved for the invoking block
    {
        void IfTrue()
        {
            foreach(Block bloc in block.ifTrue)
            {
                BlockAction(bloc);
            }
        }
        void IfFalse()
        {
            foreach (Block bloc in block.ifFalse)
            {
                BlockAction(bloc);
            }
        }
        if (block.type == Types.Value)
        {
            Debug.LogError("Value cant be an output!");
            return;
        } else if (block.type == Types.Logic)
        {
            if(block.inputs.Count != 2 || block.inputs.Count != 2) 
            {
                Debug.LogError("Invalid Number of inputs!");
            }
            int a = block.inputs[1].values[block.inputsIds[0]];
            int b = block.inputs[2].values[block.inputsIds[1]];
            switch (block.operato)
            {
            case Operator.equal:
                if(a == b)
                {
                    IfTrue();
                }
                else {          
                    IfFalse();
                }
                break;
            case Operator.greater:
                if(a > b)
                {
                    IfTrue();
                }
                else
                {
                    IfFalse();
                }
                break;
            case Operator.less:
                if (a < b)
                {
                    IfTrue();
                }
                else
                {
                    IfFalse();
                }
                break;
            case Operator.lessOrEqual:
                if (a <= b)
                {
                    IfTrue();
                }
                else
                    {
                    IfFalse();
                }
                break;
            case Operator.greaterOrEqual:
                if (a >= b)
                {
                    IfTrue();
                }
                else
                    {
                    IfFalse();
                }
                break;
            }
        } else if(block.type == Types.Transfrom)
        {
            // Position.x Position.y, Rotation, Scale.x, Scale.y
            for(int i = 1; i < block.inputs.Count; i++)
            {
                Block block1 = block.inputs[i];
                if (block1.id == 0)
                {
                    transform.position = new Vector3(block1.values[block1.inputsIds[i]], transform.position.y, transform.position.z);
                }
                else if (block1.id == 1)
                {
                    transform.position = new Vector3(transform.position.x, block1.values[block1.inputsIds[i]], transform.position.z);
                }
                else if (block1.id == 2)
                {
                    transform.rotation = Quaternion.Euler(0, 0, block1.values[block1.inputsIds[i]]);
                }
                else if (block1.id == 3)
                {
                    transform.localScale = new Vector3(block1.values[block1.inputsIds[i]], transform.localScale.y, transform.localScale.z);
                }
                else if (block1.id == 4)
                {
                    transform.localScale = new Vector3(transform.localScale.x, block1.values[block1.inputsIds[i]], transform.localScale.z);
                }
            }
        }
    }
}
public enum Types
{
    Value,
    Logic,
    Operation,
    Key,
    Transfrom,
    Rigidbody,
    Sprite
}
public enum Operator
{
    equal,
    greater,
    less,
    greaterOrEqual,
    lessOrEqual,
}
[Serializable]
public struct Block
{
    public int id;
    public Types type;
    public Operator operato;

    public int[] values;

    public List<Block> inputs;
    public List<int> inputsIds;

    public List<Block> ifTrue;
    public List<Block> ifFalse;
}
