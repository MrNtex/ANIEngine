using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorWheel : MonoBehaviour, IPointerClickHandler
{
    public Image colorWheelImage;
    public RectTransform wheelRectTransform;
    public delegate void ColorChanged(Color newColor);
    public event ColorChanged OnColorChanged;

    private Texture2D colorWheelTexture;

    void Start()
    {
        // Convert the color wheel image to a Texture2D for pixel reading
        colorWheelTexture = ConvertToTexture2D(colorWheelImage.sprite);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the click is within the color wheel bounds
        if (RectTransformUtility.RectangleContainsScreenPoint(wheelRectTransform, eventData.position, eventData.pressEventCamera))
        {
            Color selectedColor = GetColorFromWheel(eventData.position);
            OnColorChanged?.Invoke(selectedColor);
        }
    }

    private Color GetColorFromWheel(Vector2 screenPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(wheelRectTransform, screenPosition, null, out Vector2 localPoint);
        Vector2 normalizedPoint = Rect.PointToNormalized(wheelRectTransform.rect, localPoint);

        int x = Mathf.FloorToInt(colorWheelTexture.width * normalizedPoint.x);
        int y = Mathf.FloorToInt(colorWheelTexture.height * normalizedPoint.y);

        return colorWheelTexture.GetPixel(x, y);
    }

    private Texture2D ConvertToTexture2D(Sprite sprite)
    {
        Texture2D originalTexture = sprite.texture;
        Rect spriteRect = sprite.textureRect;
        Texture2D newTexture = new Texture2D((int)spriteRect.width, (int)spriteRect.height);

        // Copy the pixels from the sprite's texture to the new texture
        Color[] pixels = originalTexture.GetPixels(
            (int)spriteRect.x,
            (int)spriteRect.y,
            (int)spriteRect.width,
            (int)spriteRect.height);

        newTexture.SetPixels(pixels);
        newTexture.Apply();

        return newTexture;
    }

}
