using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Connector : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public bool isDragging = false;
    [SerializeField]
    private MovableModalWindow[] movableModalWindows;

    private NodeOutput nodeOutput;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isDragging && eventData.pointerCurrentRaycast.gameObject.CompareTag("ScriptingOutput"))
        {
            foreach(MovableModalWindow movableModalWindow in movableModalWindows)
            {
                movableModalWindow.enabled = false;
            }
            nodeOutput = eventData.pointerCurrentRaycast.gameObject.GetComponent<NodeOutput>();
            nodeOutput.lr.enabled = true;
            isDragging = true;
            Debug.Log("Drag Started");
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 screenPoint = eventData.position; // Get current screen position of the mouse
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPoint);

            // Now use worldPosition to set your node positions or handle dragging
            nodeOutput.SetPositions(worldPosition); // Assuming SetPositions() expects a world position

            Debug.Log("Dragging at world position: " + worldPosition);
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
        if(isDragging)
        {
            foreach (MovableModalWindow movableModalWindow in movableModalWindows)
            {
                movableModalWindow.enabled = true;
            }
            isDragging = false;
            nodeOutput.lr.enabled = false; // Overriden by CreateConnection() if a connection is made
            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ScriptingInput"))
            {
                Node nodeInput = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.GetComponent<Node>();
                nodeOutput.CreateConnection(nodeInput);
                nodeOutput.lr.SetPosition(1, eventData.pointerCurrentRaycast.gameObject.transform.position); // Set the line to the input position
            }
            Debug.Log("Drag Ended");
        }
        
    }

}
