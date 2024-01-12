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

            //AutoSync to solve a bug causing colliders not to move when draggin
            //https://forum.unity.com/threads/physics2d-auto-sync-transforms.108393/
            Physics2D.autoSyncTransforms = false;
        }
        else
        {
            timeScaleCashed = Time.timeScale;
            Time.timeScale = 0;
            playButton.color = colors[1];

            Physics2D.autoSyncTransforms = true;
        }
    }
}
