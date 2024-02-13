using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntryNode : Node
{
    public EntryType entryType;
    //[SerializeField]
    public TMP_Dropdown entryDropdown;

    public NodesCreator nodesCreator;
    override public bool? Execute()
    {
        return null;
        //Debug.Log("EntryNode!");
    }
    override public object GetValue()
    {
        return 0;
        // Do nothing
    }
    public void SetEntry()
    {
        entryType = (EntryType)entryDropdown.value;
        nodesCreator.ChangeEntryNodes(this);
    }

}
public enum EntryType
{
    Update,
    Start
}
