using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldScript))]
public class CustomWorldInspector : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WorldScript w = (WorldScript) target;
        if (GUILayout.Button("Generate!"))
        {
            w.GenerateWorld();
        }
    }
}
