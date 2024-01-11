using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectSettingsMng : MonoBehaviour
{
    Camera mainCamera;

    [Header("World")]
    [SerializeField]
    private TMP_InputField[] bgColor;
    [SerializeField]
    private Image bgColorPreview;

    [SerializeField]
    private TMP_InputField gravity;
    [SerializeField]
    private TMP_InputField timeScale;
    [Header("Camera")]
    [SerializeField]
    private TMP_InputField camSize;

    private bool basicUpdate = true;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main; 
    }
    private void OnEnable()
    {
        UpdateData();
        basicUpdate = false;
    }
    private void UpdateData()
    {
        bgColor[0].text = (mainCamera.backgroundColor.r * 255).ToString("0");
        bgColor[1].text = (mainCamera.backgroundColor.g * 255).ToString("0");
        bgColor[2].text = (mainCamera.backgroundColor.b * 255).ToString("0");
        
        Color color = mainCamera.backgroundColor;
        color.a = 1;
        bgColorPreview.color = color;

        gravity.text = Physics2D.gravity.y.ToString();
        timeScale.text = PlayMode.timeScaleCashed.ToString();
        camSize.text = mainCamera.orthographicSize.ToString();

    }

    public void UpdateWorld()
    {
        if(basicUpdate)
        {
            return;
        }
        if (!ValidateInputs()) return;

        float r = float.Parse(bgColor[0].text) / 255;
        float g = float.Parse(bgColor[1].text) / 255;
        float b = float.Parse(bgColor[2].text) / 255;

        mainCamera.backgroundColor = new Color(r, g, b);

        Physics2D.gravity = new Vector2(0, float.Parse(gravity.text));
        PlayMode.timeScaleCashed = float.Parse(timeScale.text);
        if(Time.timeScale != 0)
        {
            Time.timeScale = PlayMode.timeScaleCashed;
        }

        mainCamera.orthographicSize = float.Parse(camSize.text);
    }
    private bool ValidateInputs()
    {
        // Check if RGB values are within range and if parsing is successful
        foreach (var colorInput in bgColor)
        {
            if (!TryParseColorValue(colorInput.text, out _))
                return false;
        }

        // Validate gravity and timeScale
        if (!float.TryParse(gravity.text, out float gravityValue) || gravityValue > 100 || gravityValue < -100)
            return false;
        if (!float.TryParse(timeScale.text, out float timeScaleValue) || timeScaleValue < 0 || timeScaleValue > 10)
            return false;
        if (!float.TryParse(camSize.text, out float fovValue) || fovValue <= 0 || fovValue > 100)
            return false;

        return true;
    }

    private bool TryParseColorValue(string input, out float value)
    {
        if (float.TryParse(input, out value))
        {
            return value >= 0 && value <= 255;
        }
        return false;
    }
}
