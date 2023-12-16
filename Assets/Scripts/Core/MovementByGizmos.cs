using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum myTypeGizmos
{
    Move,
    Rotate,
    Scale
}
public class MovementByGizmos : MonoBehaviour
{
    public myTypeGizmos gizmoType;
    public Vector2 direction;
}
