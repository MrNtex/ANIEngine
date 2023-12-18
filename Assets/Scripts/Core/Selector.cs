using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class Selector : MonoBehaviour
{
    [SerializeField]
    private Gizmos gizmos;

    private MovementByGizmos movementByGizmos;

    public GameObject gizmosObject;
    [SerializeField]
    private GameObject[] gizmosTypesObjs;
    Vector2 diffrence, startScale, startCamPosition;
    float angularDiffrence, startAngle;
    private bool isDragging = false, isDraggingCam;

    private bool isMultipleSelect = false;
    private Vector2[] multipleSelectPositions = new Vector2[2];
    private LineRenderer lineRenderer;

    public static GameObject itemSelected;
    [SerializeField]
    private Inspector inspector;
    [SerializeField]
    private TransformComp transformComp;

    private GameObject itemSelectedPivot;

    private GameObject cameraMain;

    private void Start()
    {
        cameraMain = Camera.main.gameObject;
        lineRenderer = GetComponent<LineRenderer>();
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
                        ItemSelectedChanged(hitInfo.collider.gameObject);
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
                        SpawnGizmos(false);
                        ItemSelectedChanged(null);
                        break;
                }
            }
            else
            {
                if(EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                SpawnGizmos(false);
                ItemSelectedChanged(null);
                if (itemSelectedPivot != null)
                {
                    itemSelectedPivot.transform.DetachChildren();

                    Destroy(itemSelectedPivot);
                    ItemSelectedChanged(null);
                    itemSelectedPivot = null;
                }
                isMultipleSelect = true;
                multipleSelectPositions[0] = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Initialize the line renderer
                lineRenderer.positionCount = 4;
                lineRenderer.loop = true; // Connects the last point to the first
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
                transformComp.UpdateTransform(itemSelected);
            }
            else if (isMultipleSelect)
            {
                multipleSelectPositions[1] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            diffrence = Vector2.zero;

            lineRenderer.positionCount = 0;
            isMultipleSelect = false;
            if (Vector2.Distance(multipleSelectPositions[0], multipleSelectPositions[1]) > 0.5f) 
            {
                Vector2[] positions = new Vector2[2];
                positions[0] = multipleSelectPositions[0];
                positions[1] = multipleSelectPositions[1];
                multipleSelectPositions[0] = Vector2.zero;
                multipleSelectPositions[1] = Vector2.zero;
                
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Selectable");
                // Calculate the bottom-left corner
                Vector2 bottomLeft = new Vector2(Mathf.Min(positions[0].x, positions[1].x), Mathf.Min(positions[0].y, positions[1].y));

                // Calculate the size
                Vector2 size = new Vector2(Mathf.Abs(positions[0].x - positions[1].x), Mathf.Abs(positions[0].y - positions[1].y));

                // Create the Rect
                Rect selectionRect = new Rect(bottomLeft, size);

                itemSelectedPivot = new GameObject();
                List<GameObject> selectedObjects = new List<GameObject>();
                foreach (var item in objects)
                {
                    if (selectionRect.Contains(item.transform.position))
                    {
                        selectedObjects.Add(item);
                        //item.transform.SetParent(itemSelectedPivot.transform);
                    }
                }
                if(selectedObjects.Count > 0)
                {
                    Vector2 center = Vector2.zero;
                    foreach (GameObject child in selectedObjects)
                    {
                        center += (Vector2)child.transform.position;
                        
                    }
                    center = new Vector2(center.x/ selectedObjects.Count, center.y/ selectedObjects.Count);

                    itemSelected = itemSelectedPivot;
                    SpawnGizmos(true);
                    gizmosObject.transform.position = new Vector3(center.x, center.y, -1.5f);
                    itemSelectedPivot.transform.position = new Vector3(center.x, center.y, itemSelectedPivot.transform.position.z);
                    foreach (GameObject child in selectedObjects)
                    {
                        child.transform.SetParent(itemSelectedPivot.transform);
                    }
                }
                else
                {
                    // Nothing was selected :c
                    Destroy(itemSelectedPivot);
                }
                
            }
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
            cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + startCamPosition.x - mousePosition.x, cameraMain.transform.position.y + startCamPosition.y - mousePosition.y, cameraMain.transform.position.z);
        }
        if(Input.GetMouseButtonUp(2))
        {
            // Camera movement by mouse
            isDraggingCam = false;
        }

        if(isMultipleSelect && Mathf.Abs(multipleSelectPositions[0].x - multipleSelectPositions[1].x) > .01f && Mathf.Abs(multipleSelectPositions[0].y - multipleSelectPositions[1].y) > .01f)
        {
            // Calculate the bottom-left corner
            Vector2 bottomLeft = new Vector2(Mathf.Min(multipleSelectPositions[0].x, multipleSelectPositions[1].x), Mathf.Min(multipleSelectPositions[0].y, multipleSelectPositions[1].y));

            // Calculate the size
            Vector2 size = new Vector2(Mathf.Abs(multipleSelectPositions[0].x - multipleSelectPositions[1].x), Mathf.Abs(multipleSelectPositions[0].y - multipleSelectPositions[1].y));

            // Create the Rect
            Rect selectionRect = new Rect(bottomLeft, size);

            // Draw the Rect on the screen
            //GUI.Box(selectionRect, "");
            lineRenderer.SetPosition(0, new Vector3(selectionRect.xMin, selectionRect.yMin, -1));
            lineRenderer.SetPosition(1, new Vector3(selectionRect.xMax, selectionRect.yMin, -1));
            lineRenderer.SetPosition(2, new Vector3(selectionRect.xMax, selectionRect.yMax, -1));
            lineRenderer.SetPosition(3, new Vector3(selectionRect.xMin, selectionRect.yMax, -1));
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
    void ItemSelectedChanged(GameObject item)
    {
        itemSelected = item;
        inspector.UpdateInspector(item);
    }
}
