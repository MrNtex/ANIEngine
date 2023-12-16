using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField]
    private Gizmos gizmos;

    private MovementByGizmos movementByGizmos;
    private GameObject gizmosObject;
    Vector2 diffrence;
    private bool isDragging = false;


    public GameObject itemSelected;

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(ray, Vector2.zero);

            if (hitInfo.collider != null)
            {
                Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
                switch (hitInfo.collider.gameObject.layer)
                {
                    case 5:
                        // UI
                        break;
                    case 6:
                        itemSelected = hitInfo.collider.gameObject;
                        break;
                    case 7:
                        isDragging = false;
                        movementByGizmos = hitInfo.collider.gameObject.GetComponent<MovementByGizmos>();
                        gizmosObject = hitInfo.collider.transform.parent.gameObject;
                        diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - itemSelected.transform.position;
                        isDragging = true;
                        break;
                    default:
                        itemSelected = null;
                        break;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (itemSelected != null && movementByGizmos != null && isDragging)
            {
                if (movementByGizmos.gizmoType == myTypeGizmos.Move)
                {
                    Vector3 mousePosition = Input.mousePosition;
                    Vector2 moveBy = Camera.main.ScreenToWorldPoint(mousePosition);


                    moveBy -= new Vector2(diffrence.x * movementByGizmos.direction.x, diffrence.y * movementByGizmos.direction.y);
                    
                    // Values of direction are 0 or 1, so we can use them to multiply the position of the object
                    Vector2 movement = new Vector2(movementByGizmos.direction.x == 1 ? moveBy.x : itemSelected.transform.position.x, movementByGizmos.direction.y == 1 ? moveBy.y : itemSelected.transform.position.y);

                    Debug.Log("Diffrence: " + diffrence + ",  " + movementByGizmos.direction + ",  " + movement);
                    itemSelected.transform.position = movement;
                    gizmosObject.transform.position = movement;
                }
            }
        }
    }
    
}
