using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptingManager : MonoBehaviour
{
    [SerializeField]
    private NodesCreator nodesCreator;

    [SerializeField]
    private GameObject scriptModalPrefab;
    [SerializeField]
    private TMP_Text header;

    [SerializeField]
    private Selector selector;
    public void GoToScript()
    {
        GameObject obj = Selector.itemSelected;
        ObjectScripting objectScripting = obj.GetComponent<ObjectScripting>();
        if (obj != null)
        {
            nodesCreator.gameObject.SetActive(true);
            if (nodesCreator.childrensScripts.Contains(obj))
            {
                // Object already has a script, open it
                nodesCreator.currentObject = nodesCreator.childrensScripts.IndexOf(obj);
            }
            else
            {
                GameObject newScript = Instantiate(scriptModalPrefab, nodesCreator.transform);
                newScript.name = $"{obj.name}'s Script";
                objectScripting.script = newScript;
                nodesCreator.childrensScripts.Add(obj);
                nodesCreator.currentObject = nodesCreator.childrensScripts.Count - 1;
                

            }
            header.text = $"{obj.name}'s Script";
            objectScripting.script.SetActive(true);
            selector.enabled = false;
        }
    }
    public void CloseScript()
    {
        nodesCreator.childrensScripts[nodesCreator.currentObject].GetComponent<ObjectScripting>().script.SetActive(false);
        nodesCreator.gameObject.SetActive(false);
        
    }
}
