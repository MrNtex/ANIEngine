using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class Connector : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public bool isDragging = false;
    
    public List<MovableModalWindow> movableModalWindows = new List<MovableModalWindow>();

    private NodeOutput nodeOutput;

    private InputType inputType;

    private int sourceId;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isDragging && eventData.pointerCurrentRaycast.gameObject.CompareTag("ScriptingOutput"))
        {
            foreach(MovableModalWindow movableModalWindow in movableModalWindows)
            {
                movableModalWindow.enabled = false;
            }
            
            nodeOutput = eventData.pointerCurrentRaycast.gameObject.GetComponent<NodeOutput>();
            
            inputType = nodeOutput.inputType;
            sourceId = nodeOutput.id;
            
            
            nodeOutput.lr.enabled = true;
            nodeOutput.SetPositions(Vector2.zero, true);// Set the line to the output position
            isDragging = true;
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                nodeOutput.DestroyConnections();
                return;
            }
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

        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
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
                GameObject input = eventData.pointerCurrentRaycast.gameObject;
                Node nodeInput = input.GetComponentInParent<Node>();
                //Debug.Log(input.GetComponent<InputOutputData>().inputType);
                if(input.GetComponent<InputOutputData>().inputType == inputType)
                {
                    Debug.Log($"NodeInput: {nodeInput}, transfrom {input.transform}, sourceId: {sourceId}, targetId {input.GetComponent<InputOutputData>().id}");
                    nodeOutput.CreateConnection(nodeInput, input.transform, sourceId, input.GetComponent<InputOutputData>().id);
                    nodeOutput.lr.SetPosition(1, eventData.pointerCurrentRaycast.gameObject.transform.position); // Set the line to the input position
                }
            }
            Debug.Log("Drag Ended");
        }
        
    }

}
