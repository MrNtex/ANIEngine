using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : MonoBehaviour
{
    public RectTransform startPoint;
    public RectTransform endPoint;
    private RectTransform lineTransform;
    private Image lineImage;
    // Start is called before the first frame update
    void Start()
    {
        lineTransform = GetComponent<RectTransform>();
        lineImage = GetComponent<Image>();
        DrawALine();
    }

    public void DrawALine(Vector2 a, Vector2 b)
    {
        // Determine the position of the line
        transform.position = a;

        // Calculate the distance and direction between the start and end points
        float distance = Vector3.Distance(a, b);
        lineTransform.sizeDelta = new Vector2(distance, lineImage.sprite.texture.height); // Adjust the height based on your line texture or preference

        // Calculate the angle to rotate the line
        Vector3 direction = (b - a).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
