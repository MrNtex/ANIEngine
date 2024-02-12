using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodesCreator : MonoBehaviour, IPointerDownHandler
{
    // Hierarchical data structure
    // Object in the scene -> ObjectScripting -> script -> Connector -> MovableModalWindow
    [SerializeField] 
    private GameObject[] nodePrefabs;

    public GameObject[] childrensScripts;
    public int currentObject;

    private Connector connector;
    private ObjectScripting objectScripting;

    [SerializeField]
    private GameObject spawnModal;

    private Node updateNode, startNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddNode(int idx)
    {
        objectScripting = childrensScripts[currentObject].GetComponent<ObjectScripting>();
        connector = objectScripting.script.GetComponent<Connector>();
        GameObject newNode = Instantiate(nodePrefabs[idx], objectScripting.script.transform);
        if(idx == 0)
        {
            if(updateNode == null)
            {
                updateNode = newNode.GetComponent<Node>();
                objectScripting.updateNode = updateNode;
            }else if(startNode == null)
            {
                startNode = newNode.GetComponent<Node>();
                objectScripting.startNode = startNode;
                EntryNode entryNode = newNode.GetComponent<EntryNode>();
                entryNode.entryType = EntryType.Start;
                entryNode.entryDropdown.value = 1;
                entryNode.entryDropdown.interactable = false;
            }
            else
            {
                //Button should be disabled do nothing
                Destroy(newNode);
                return;
            }
        }
        connector.movableModalWindows.Add(newNode.GetComponent<MovableModalWindow>());
        spawnModal.SetActive(false);
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            spawnModal.SetActive(true);
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
