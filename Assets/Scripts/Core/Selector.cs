using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    [SerializeField]
    private Gizmos gizmos;

    private MovementByGizmos movementByGizmos;
    [SerializeField]
    private GameObject gizmosObject;
    [SerializeField]
    private GameObject[] gizmosTypesObjs;
    Vector2 diffrence, startScale, startCamPosition;
    float angularDiffrence, startAngle;
    private bool isDragging = false, isDraggingCam;


    public GameObject itemSelected;

    private GameObject cameraMain;

    private void Start()
    {
        cameraMain = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(ray, Vector2.zero);

            if (hitInfo.collider != null)
            {
                
                switch (hitInfo.collider.gameObject.layer)
                {
                    case 6:
                        itemSelected = hitInfo.collider.gameObject;
                        SpawnGizmos(true);

                        gizmosObject.transform.position = new Vector3(itemSelected.transform.position.x, itemSelected.transform.position.y, -1.5f);
                        break;
                    case 7:
                        movementByGizmos = hitInfo.collider.gameObject.GetComponent<MovementByGizmos>();
                        gizmosObject = hitInfo.collider.transform.root.gameObject;
                        diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - itemSelected.transform.position;
                        angularDiffrence = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
                        startAngle = Quaternion.Angle(Quaternion.identity, itemSelected.transform.rotation);
                        startScale = itemSelected.transform.localScale;

                        isDragging = true;

                        break;
                    default:
                        Debug.Log("Default");
                        SpawnGizmos(false);
                        break;
                }
            }
            else
            {
                if(EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("UI");
                    return;
                }
                SpawnGizmos(false);
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

                    // Debug.Log("Diffrence: " + diffrence + ",  " + movementByGizmos.direction + ",  " + movement);
                    itemSelected.transform.position = movement;
                    gizmosObject.transform.position = movement;
                }
                if(movementByGizmos.gizmoType == myTypeGizmos.Rotate)
                {
                    
                    Vector3 mousePosition = Input.mousePosition;
                    Vector2 moveBy = Camera.main.ScreenToWorldPoint(mousePosition);
                    Vector2 movement = new Vector2(moveBy.x - itemSelected.transform.position.x, moveBy.y - itemSelected.transform.position.y);
                    float angle = startAngle + Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - angularDiffrence;
                    //itemSelected.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    itemSelected.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                if(movementByGizmos.gizmoType == myTypeGizmos.Scale)
                {
                    Vector3 mousePosition = Input.mousePosition;
                    Vector2 moveBy = Camera.main.ScreenToWorldPoint(mousePosition);
                    moveBy -= (Vector2)itemSelected.transform.position;
                    moveBy -= new Vector2(diffrence.x * movementByGizmos.direction.x, diffrence.y * movementByGizmos.direction.y);
                    // Values of direction are 0 or 1, so we can use them to multiply the position of the object
                    Vector2 movement = new Vector2(movementByGizmos.direction.x == 1 ? startScale.x + moveBy.x : itemSelected.transform.localScale.x, movementByGizmos.direction.y == 1 ? startScale.y + moveBy.y : itemSelected.transform.localScale.y);
                    itemSelected.transform.localScale = new Vector3(movement.x, movement.y, 1);
                    //gizmosObject.transform.localScale = new Vector3(distance, distance, 1);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            diffrence = Vector2.zero;
        }
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            if(itemSelected != null)
            {
                Destroy(itemSelected);
                itemSelected = null;
                SpawnGizmos(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            itemSelected = null;
            SpawnGizmos(false);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(itemSelected != null)
            {
                itemSelected.transform.rotation = Quaternion.identity;
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(itemSelected != null)
            {
                itemSelected.transform.localScale = Vector3.one;
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(itemSelected != null)
            {
                itemSelected.transform.localScale = new Vector3(-itemSelected.transform.localScale.x, itemSelected.transform.localScale.y, itemSelected.transform.localScale.z);
            }
        }

        if(Input.GetMouseButtonDown(2))
        {
            // Camera movement by mouse
            if (!isDraggingCam)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startCamPosition = new Vector3(mousePosition.x, mousePosition.y, cameraMain.transform.position.z);
            }
            
            isDraggingCam = true;
        }
        if(Input.GetMouseButton(2))
        {
            // Camera movement by mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePosition);
            cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + startCamPosition.x - mousePosition.x, cameraMain.transform.position.y + startCamPosition.y - mousePosition.y, cameraMain.transform.position.z);
        }
        if(Input.GetMouseButtonUp(2))
        {
            // Camera movement by mouse
            Debug.Log("Mouse up");
            isDraggingCam = false;
        }
    }
    public void SpawnGizmos(bool spawn)
    {
        if (!spawn)
        {
            gizmosObject.SetActive(false);
            return;
        }
        gizmosObject.SetActive(true);
        foreach (var item in gizmosTypesObjs)
        {
            item.SetActive(false);
        }
        switch (gizmos.gizmoType)
        {
            case GTypes.Move:
                gizmosTypesObjs[0].SetActive(true);
                break;
            case GTypes.Rotate:
                gizmosTypesObjs[1].SetActive(true);
                break;
            case GTypes.Scale:
                gizmosTypesObjs[2].SetActive(true);
                gizmosTypesObjs[2].transform.rotation = Quaternion.Euler(0, 0, itemSelected.transform.rotation.eulerAngles.z);
                break;
        }
    }
}
