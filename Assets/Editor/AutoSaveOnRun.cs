using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AutosaveOnRun{

    static AutosaveOnRun(){

        EditorApplication.playmodeStateChanged = () =>
        {
            if(EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying){

                Debug.Log("AutoSaving scene");

                EditorApplication.SaveScene();
                //EditorApplication.SaveAssets();
            }
        };
    }
}