using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyNode : Node
{
    public Rigidbody2D myRigidBody;


    public override bool? Execute()
    {
        if (Inputs[1] != null) // Position X
        {
            float x = (float)Inputs[1].myNode.GetValue(Inputs[1].id);
            myRigidBody.velocity = new Vector2(x, myRigidBody.velocity.y);
        }
        if (Inputs[2] != null) // Position Y
        {
            float y = (float)Inputs[2].myNode.GetValue(Inputs[2].id);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, y);
        }
        if (Inputs[3] != null) // Rotation
        {
            float z = (float)Inputs[3].myNode.GetValue(Inputs[3].id);
            myRigidBody.angularVelocity = z;
        }
        if (Inputs[4] != null || Inputs[5] != null) // Add force
        {
            Vector2 force = new Vector2(Inputs[4] == null ? 0.0f : (float)Inputs[4].myNode.GetValue(Inputs[4].id), Inputs[5] == null ? 0.0f : (float)Inputs[5].myNode.GetValue(Inputs[5].id));
            myRigidBody.AddForce(force);
        }
        if (Inputs[6] != null || Inputs[7] != null) // Add force
        {
            Vector2 movePosition = new Vector2(Inputs[6] == null ? 0.0f : (float)Inputs[6].myNode.GetValue(Inputs[6].id), Inputs[7] == null ? 0.0f : (float)Inputs[7].myNode.GetValue(Inputs[7].id));
            myRigidBody.MovePosition(movePosition);
        }
        return null;
    }
    public override object GetValue(int id)
    {
        switch (id)
        {
            case 0:
                return myRigidBody.velocity.x;
            case 1:
                return myRigidBody.velocity.y;
            case 2:
                return myRigidBody.angularVelocity;
        }
        Debug.LogError("Invalid id");
        return null;
    }
}
