using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldScript))]
public class CustomWorldInspector : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WorldScript w = (WorldScript) target;
        GUILayout.Label("Children: "+w.transform.childCount +"");
        if (GUILayout.Button("Generate!"))
        {
            w.GenerateWorld();
        }
    }
}
