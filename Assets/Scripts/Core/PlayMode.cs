using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMode : MonoBehaviour
{
    Image playButton;
    [SerializeField]
    private Color[] colors;
    private void Start()
    {
        playButton = GetComponent<Image>();
        Play();
    }
    public void Play()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            playButton.color = colors[0];
        }
        else
        {
            Time.timeScale = 0;
            playButton.color = colors[1];
        }
    }
}
