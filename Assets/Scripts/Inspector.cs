using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    Selector selector;

    TransformComp transformComp;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        transformComp = GetComponent<TransformComp>();
    }
    public void UpdateInspector(GameObject source)
    {
        transformComp.UpdateTransform(source);
    }
}
