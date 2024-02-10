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
        int GetValue(Block block, int id)
        {
            if(block.type == Types.Value)
            {
                return block.values[0];
            }
            if(block.type == Types.Operation)
            {
                int a = GetValue(block.inputs[0], block.inputsIds[0]);
                int b = GetValue(block.inputs[1], block.inputsIds[1]);
                switch (block.operation)
                {
                    case Operation.add:
                        return a + b;
                    case Operation.subtract:
                        return a - b;
                    case Operation.multiply:
                        return a * b;
                    case Operation.divide:
                        return a / b;
                    case Operation.modulo:
                        return a % b;
                }
            }
            if(block.type == Types.Transfrom)
            {
                if(id == 0)
                {
                    return (int)transform.position.x;
                }
                if (id == 1)
                {
                    return (int)transform.position.y;
                }
                if (id == 2)
                {
                    return (int)transform.rotation.z;
                }
                if (id == 3)
                {
                    return (int)transform.localScale.x;
                }
                if (id == 4)
                {
                    return (int)transform.localScale.y;
                }
            }
            return block.values[block.inputsIds[0]];
        }
        if (block.type == Types.Value)
        {
            Debug.LogError("Value cant be an output!");
            return;
        } else if (block.type == Types.Logic)
        {
            if (block.inputs.Count != 2 || block.inputs.Count != 2)
            {
                Debug.LogError("Invalid Number of inputs!");
            }
            int a = GetValue(block.inputs[1], block.inputsIds[0]);
            int b = GetValue(block.inputs[2], block.inputsIds[1]);
            switch (block.operato)
            {
                case Operator.equal:
                    if (a == b)
                    {
                        IfTrue();
                    }
                    else {
                        IfFalse();
                    }
                    break;
                case Operator.greater:
                    if (a > b)
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
        } else if (block.type == Types.Key) {
            if(Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), block.key)))
            {
                IfTrue();
            }else
            {
                IfFalse();
            }
        } else if (block.type == Types.Transfrom)
        {
            // Position.x Position.y, Rotation, Scale.x, Scale.y
            for (int i = 1; i < block.inputs.Count; i++)
            {
                int val = GetValue(block.inputs[i], block.inputs[i].inputsIds[i - 1]);
                if (block.outputs[i] == 0)
                {
                    transform.position = new Vector3(val, transform.position.y, transform.position.z);
                }
                else if (block.outputs[i] == 1)
                {
                    transform.position = new Vector3(transform.position.x, val, transform.position.z);
                }
                else if (block.outputs[i] == 2)
                {
                    transform.rotation = Quaternion.Euler(0, 0, val);
                }
                else if (block.outputs[i] == 3)
                {
                    transform.localScale = new Vector3(val, transform.localScale.y, transform.localScale.z);
                }
                else if (block.outputs[i] == 4)
                {
                    transform.localScale = new Vector3(transform.localScale.x, val, transform.localScale.z);
                }
            }
        }
    }
}
public enum Types
{
    Value,
    Logic, //Invoker
    Operation,
    Key, //Invoker
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
public enum Operation
{
    add,
    subtract,
    multiply,
    divide,
    modulo
}
[Serializable]
public struct Block
{
    public int id;
    public Types type;
    public Operator operato;
    public Operation operation;

    public int[] values;

    public List<Block> inputs;
    public List<int> inputsIds;
    public List<int> outputs;

    public List<Block> ifTrue;
    public List<Block> ifFalse;

    public string key;
}
