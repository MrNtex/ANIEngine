using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{
    static AutoSave()
    {
        EditorApplication.playModeStateChanged += SaveOnPlay;
    }

    private static void SaveOnPlay(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            Debug.Log("AutoSave: Saving scene before entering Play mode: " + System.DateTime.Now);
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }
            
    }
}
