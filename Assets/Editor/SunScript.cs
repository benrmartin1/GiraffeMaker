using UnityEngine;
using UnityEditor;

public class SunScript : Editor
{
    [MenuItem("Healpers/SetLightDirection")]
    static void SetDirection()
    {
        Transform selected = Selection.transforms[0];
        if (!selected) return;
        selected.forward = -SceneView.lastActiveSceneView.camera.transform.forward;
    }
}