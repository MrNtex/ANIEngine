using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodesCreator : MonoBehaviour, IPointerDownHandler
{
    // Hierarchical data structure
    // Object in the scene -> ObjectScripting -> script -> Connector -> MovableModalWindow
    [SerializeField] 
    private GameObject[] nodePrefabs;

    public List<GameObject> childrensScripts = new List<GameObject>();
    public int currentObject;

    private Connector connector;
    private ObjectScripting objectScripting;

    [SerializeField]
    private GameObject spawnModal;
    [SerializeField]
    private Button rbButton;

    private Node updateNode, startNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ChangeEntryNodes(EntryNode entryNode)
    {
        if(entryNode.entryType == EntryType.Start)
        {
            if(objectScripting.updateNode != null)
            {
                updateNode = null;
                objectScripting.updateNode = null;
            }
            startNode = entryNode;
            objectScripting.startNode = entryNode;
        }
        else
        {
            if (objectScripting.startNode != null)
            {
                startNode = null;
                objectScripting.startNode = null;
            }
            updateNode = entryNode;
            objectScripting.updateNode = entryNode;
        }
    }
    public void AddNode(int idx)
    {
        
        objectScripting = childrensScripts[currentObject].GetComponent<ObjectScripting>();
        if (objectScripting.GetComponent<Rigidbody2D>() == null && idx == 8)
        {
            //Button should be disabled do nothing
            return;
        }
        connector = objectScripting.script.GetComponent<Connector>();
        GameObject newNode = Instantiate(nodePrefabs[idx], objectScripting.script.transform);
        if(idx == 0)
        {
            if(updateNode == null)
            {
                updateNode = newNode.GetComponent<Node>();
                objectScripting.updateNode = updateNode;
                newNode.GetComponent<EntryNode>().nodesCreator = this;
                if(objectScripting.updateNode != null && objectScripting.startNode != null)
                {
                    newNode.GetComponent<EntryNode>().entryDropdown.interactable = false;
                    objectScripting.startNode.gameObject.GetComponent<EntryNode>().entryDropdown.interactable = false;
                }
            }else if(startNode == null)
            {
                startNode = newNode.GetComponent<Node>();
                objectScripting.startNode = startNode;
                EntryNode entryNode = newNode.GetComponent<EntryNode>();
                entryNode.entryType = EntryType.Start;
                entryNode.entryDropdown.value = 1;
                entryNode.entryDropdown.interactable = false;
                entryNode.nodesCreator = this;
                if (objectScripting.updateNode != null && objectScripting.startNode != null)
                {
                    objectScripting.updateNode.gameObject.GetComponent<EntryNode>().entryDropdown.interactable = false;
                }
            }
            else
            {
                //Button should be disabled do nothing
                Destroy(newNode);
                return;
            }
        }else if(idx == 7)
        {
            //TRANSFORM NODE
            newNode.GetComponent<TransformNode>().myTransform = objectScripting.transform;
        }else if(idx == 8)
        {
            //RIGIDBODY NODE
            newNode.GetComponent<RigidbodyNode>().myRigidBody = objectScripting.GetComponent<Rigidbody2D>();
        }
        connector.movableModalWindows.Add(newNode.GetComponent<MovableModalWindow>());
        spawnModal.SetActive(false);
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            spawnModal.SetActive(true);
            objectScripting = childrensScripts[currentObject].GetComponent<ObjectScripting>();
            if (objectScripting.GetComponent<Rigidbody2D>() == null)
            {
                rbButton.interactable = false;
            }
            else
            {
                rbButton.interactable = true;
            }
            Vector3 screenPoint = eventData.position; // Get current screen position of the mouse
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPoint);
            spawnModal.transform.position = new Vector2(worldPosition.x, worldPosition.y);
        }
        else
        {
            if(!eventData.pointerCurrentRaycast.gameObject.CompareTag("NodesSpawner"))
            {
                spawnModal.SetActive(false);
            }
        }
    }
}
