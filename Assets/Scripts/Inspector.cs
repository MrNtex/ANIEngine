using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
    Selector selector;

    TransformComp transformComp;
    ObjectBehaviour objectBehaviour;

    [SerializeField]
    private Image colorPreview;

    [SerializeField]
    private TMP_InputField[] colorInputs;
    // Start is called before the first frame update
    [SerializeField]
    private TMP_Dropdown bodyType, colliderType;
    [SerializeField]
    private TMP_InputField mass, linearDrag, angularDrag;

    [SerializeField]
    private GameObject rigidbodyInspector, colliderInspector, spriteInspector;
    // Update is called once per frame
    private void Start()
    {
        transformComp = GetComponent<TransformComp>();
    }
    public void UpdateInspector(GameObject source)
    {
        transformComp.UpdateTransform(source);

        rigidbodyInspector.SetActive(false);
        colliderInspector.SetActive(false);
        spriteInspector.SetActive(false);

        if (source == null)
        { 
            return;
        }

        objectBehaviour = source.GetComponent<ObjectBehaviour>();

        SpriteRenderer spriteRenderer = source.GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            spriteInspector.SetActive(true);
            colorPreview.color = spriteRenderer.color;

            colorInputs[0].text = (spriteRenderer.color.r * 255).ToString();
            colorInputs[1].text = (spriteRenderer.color.g * 255).ToString();
            colorInputs[2].text = (spriteRenderer.color.b * 255).ToString();
        }
        else
        {
            spriteInspector.SetActive(false);
        }
        

        if(objectBehaviour.hasRigidbody)
        {
            //Spawn rigidbody inspector
            rigidbodyInspector.SetActive(true);

            bodyType.value = (int)objectBehaviour.rb.bodyType;
            mass.text = objectBehaviour.rb.mass.ToString();
            linearDrag.text = objectBehaviour.rb.drag.ToString();
            angularDrag.text = objectBehaviour.rb.angularDrag.ToString();
        }
        if(objectBehaviour.colliderEnabled)
        {
            //Spawn collider inspector
            colliderInspector.SetActive(true);
        }
    }

    public void UpdateColor(int index)
    {
        SpriteRenderer spriteRenderer = Selector.itemSelected.GetComponent<SpriteRenderer>();
        if(colorInputs[index].text == "")
        {
            return;
        }
        float colorRGB = float.Parse(colorInputs[index].text)/255;
        if(colorRGB > 1)
        {
            colorRGB = 1;
            colorInputs[index].text = "255";
        }
        else if(colorRGB < 0)
        {
            colorRGB = 0;
        }
        Color c = new Color();
        if (index == 0)
        {
            c = new Color(colorRGB, spriteRenderer.color.g, spriteRenderer.color.b);
        }
        else if (index == 1)
        {
            c = new Color(spriteRenderer.color.r, colorRGB, spriteRenderer.color.b);
        }
        else if (index == 2)
        {
            c = new Color(spriteRenderer.color.r, spriteRenderer.color.g, colorRGB);
        }
        colorPreview.color = c;
        Selector.itemSelected.GetComponent<SpriteRenderer>().color = c;
    }
    public void UpdateBodyType()
    {
        Debug.Log(bodyType.value);
        objectBehaviour.rb.bodyType = (RigidbodyType2D)bodyType.value;
    }
    public void UpdateMass()
    {
        objectBehaviour.rb.mass = float.Parse(mass.text);
    }
    public void UpdateLinearDrag()
    {
        objectBehaviour.rb.drag = float.Parse(linearDrag.text);
    }
    public void UpdateAngularDrag()
    {
        objectBehaviour.rb.angularDrag = float.Parse(angularDrag.text);
    }

    public void SetColliderType()
    {
        if (colliderType.value == 0)
        {
            objectBehaviour.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            objectBehaviour.GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
