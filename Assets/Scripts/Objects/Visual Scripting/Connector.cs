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
    private bool isNotConnection;

    private int sourceId;
    private LineRenderer currentLineRender;
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
            isNotConnection = nodeOutput.isNotConnection;

            currentLineRender = nodeOutput.CreateANewLine();
            //nodeOutput.SetPositions(Vector2.zero, currentLineRender);// Set the line to the output position
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
            nodeOutput.SetPositions(worldPosition, currentLineRender); // Assuming SetPositions() expects a world position

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

            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ScriptingInput"))
            {
                GameObject input = eventData.pointerCurrentRaycast.gameObject;
                NodeInput nodeInput = input.GetComponent<NodeInput>();
                //Debug.Log(input.GetComponent<InputOutputData>().inputType);
                if(nodeInput.inputType == inputType)
                {
                    Debug.Log($"NodeInput: {nodeInput.myNode}, transfrom {input.transform}, sourceId: {sourceId}, targetId {nodeInput.inputID}, NOT: {isNotConnection}");
                    nodeOutput.CreateConnection(nodeInput, input.transform, sourceId, input.GetComponent<NodeInput>().inputID, isNotConnection, currentLineRender);
                    nodeOutput.SetPositions((Vector2)eventData.pointerCurrentRaycast.gameObject.transform.position, currentLineRender); // Set the line to the input position
                }
            }
            else
            {
                nodeOutput.DestroyLineRender(currentLineRender);
            }
        }
        
    }

}
