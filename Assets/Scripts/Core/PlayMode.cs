using UnityEngine;
using UnityEngine.UI;
using System; // Needed for the Action delegate

public class PlayMode : MonoBehaviour
{
    public static event Action<bool> OnPlayStateChanged;

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
        if (Time.timeScale == 0)
        {
            Time.timeScale = timeScaleCashed;
            playButton.color = colors[0];
            Physics2D.autoSyncTransforms = false;
            OnPlayStateChanged?.Invoke(true); // Invoke the event with true, indicating the game is playing
        }
        else
        {
            timeScaleCashed = Time.timeScale;
            Time.timeScale = 0;
            playButton.color = colors[1];
            Physics2D.autoSyncTransforms = true;
            OnPlayStateChanged?.Invoke(false); // Invoke the event with false, indicating the game is paused
        }
    }
}
