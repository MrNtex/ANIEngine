using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TransformComp : MonoBehaviour
{
    [SerializeField]
    private GameObject transformComponentInspector;

    [SerializeField]
    private Selector selector;

    [SerializeField]
    private TMP_InputField[] positions;
    [SerializeField]
    private TMP_InputField rotation;
    [SerializeField]
    private TMP_InputField[] scales;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateTransform(GameObject source)
    {
        if(transformComponentInspector.activeInHierarchy && source != null)
        {
            positions[0].text = source.transform.position.x.ToString();
            positions[1].text = source.transform.position.y.ToString();

            rotation.text = source.transform.rotation.eulerAngles.z.ToString();

            scales[0].text = source.transform.localScale.x.ToString();
            scales[1].text = source.transform.localScale.y.ToString();
        }
        if (!transformComponentInspector.activeInHierarchy && source != null)
        {
            transformComponentInspector.SetActive(true);
            UpdateTransform(source);
        }
        else if(source == null)
        {
            transformComponentInspector.SetActive(false);
        }
    }
    public void UpdateValue(int index)
    {
        Transform selectedObj = Selector.itemSelected.transform;
        if (index == 0)
        {
            selectedObj.position = new Vector3(float.Parse(positions[0].text), selectedObj.position.y, selectedObj.position.z);
        }
        else if (index == 1)
        {
            selectedObj.position = new Vector3(selectedObj.position.x, float.Parse(positions[1].text), selectedObj.position.z);
        }
        else if (index == 2)
        {
            selectedObj.rotation = Quaternion.Euler(0, 0, float.Parse(rotation.text));
        }
        else if (index == 3)
        {
            selectedObj.localScale = new Vector3(float.Parse(scales[0].text), selectedObj.localScale.y, selectedObj.localScale.z);
        }
        else if (index == 4)
        {
            selectedObj.localScale = new Vector3(selectedObj.localScale.x, float.Parse(scales[1].text), selectedObj.localScale.z);
        }
        selector.gizmosObject.transform.position = selectedObj.position;
    }   
}
