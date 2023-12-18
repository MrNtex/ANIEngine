using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public bool colliderEnabled = false;
    public bool hasRigidbody = false;

    public Rigidbody2D rb;

    public void ToggleCollider()
    {
        colliderEnabled = !colliderEnabled;
    }
    public void ToggleRigidbody()
    {
        hasRigidbody = !hasRigidbody;
        rb = gameObject.AddComponent<Rigidbody2D>();
    }
    public void ToggleRigidbody(bool value)
    {
        if (rb == null)
            return;
        rb.simulated = value;
    }
    public void ResetRB()
    {
        if(rb == null)
            return;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
}