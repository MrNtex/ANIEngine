using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMode : MonoBehaviour
{
    Image playButton;
    [SerializeField]
    private Color[] colors;

    public static float timeScaleCashed;
    private void Start()
    {
        timeScaleCashed = Time.timeScale;
        playButton = GetComponent<Image>();
        Play();
    }
    public void Play()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = timeScaleCashed;
            playButton.color = colors[0];
        }
        else
        {
            timeScaleCashed = Time.timeScale;
            Time.timeScale = 0;
            playButton.color = colors[1];
        }
    }
}
