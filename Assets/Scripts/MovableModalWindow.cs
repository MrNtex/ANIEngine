using UnityEngine;
using UnityEngine.EventSystems;

public class MovableModalWindow : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform dragArea;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(dragArea, eventData.position, eventData.pressEventCamera))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }
        else
        {
            offset = Vector2.zero;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (offset != Vector2.zero)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                rectTransform.localPosition = localPoint - offset;
            }
        }
    }
}
