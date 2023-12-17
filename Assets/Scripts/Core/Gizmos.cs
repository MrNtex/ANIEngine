using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GTypes
{
    Move,
    Rotate,
    Scale
}
public class Gizmos : MonoBehaviour
{
    [SerializeField]
    private Selector selector;

    public GTypes gizmoType;
    public void ChangeGizmos(int typeId) {         
        switch ((GTypes)typeId)
        {
            case GTypes.Move:
                gizmoType = (GTypes)typeId;
                break;
            case GTypes.Rotate:
                gizmoType = (GTypes)typeId;
                break;
            case GTypes.Scale:
                gizmoType = (GTypes)typeId;
                break;
        }
        selector.SpawnGizmos(true);
    }
}
