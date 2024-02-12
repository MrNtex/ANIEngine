using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntryNode : Node
{
    public EntryType entryType;
    //[SerializeField]
    public TMP_Dropdown entryDropdown;
    override public void Execute()
    {
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
    }
}
public enum EntryType
{
    Update,
    Start
}
