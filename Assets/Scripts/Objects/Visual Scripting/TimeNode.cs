using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeNode : Node
{
    bool timeSelected = true;
    [SerializeField]
    private TMP_Dropdown dropdown;
    public override bool? Execute()
    {
        return null;
    }
    public override object GetValue(int id)
    {
        if(timeSelected)
        {
            return Time.time;
        }
        else
        {
            return Time.deltaTime;
        }
    }
    public void UpdateSelected()
    {
        timeSelected = dropdown.value == 0 ? true : false;
    }
}
