using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformNode : Node
{
    public Transform myTransform;


    public override bool? Execute()
    {
        if (Inputs[1] != null) // Position X
        {
            float x = (float)Inputs[1].myNode.GetValue(Inputs[1].id);
            myTransform.position = new Vector3(x, myTransform.position.y, myTransform.position.z);
        }
        if (Inputs[2] != null) // Position Y
        {
            float y = (float)Inputs[2].myNode.GetValue(Inputs[2].id);
            myTransform.position = new Vector3(myTransform.position.x, y, myTransform.position.z);
        }
        if (Inputs[3] != null) // Rotation
        {
            float z = (float)Inputs[3].myNode.GetValue(Inputs[3].id);
            myTransform.rotation = Quaternion.Euler(0, 0, z);
        }
        if (Inputs[4] != null) // Scale X
        {
            float x = (float)Inputs[4].myNode.GetValue(Inputs[4].id);
            myTransform.localScale = new Vector3(x, myTransform.localScale.y, myTransform.localScale.z);
        }
        if (Inputs[5] != null) // Scale Y
        {
            float y = (float)Inputs[5].myNode.GetValue(Inputs[5].id);
            myTransform.localScale = new Vector3(myTransform.localScale.x, y, myTransform.localScale.z);
        }
        return null;
    }
    public override object GetValue(int id)
    {
        switch (id)
        {
            case 0:
                return myTransform.position.x;
            case 1:
                return myTransform.position.y;
            case 2:
                return myTransform.rotation.eulerAngles.z;
            case 3:
                return myTransform.localScale.x;
            case 4:
                return myTransform.localScale.y;
        }
        Debug.LogError("Invalid id");
        return null;
    }
}
