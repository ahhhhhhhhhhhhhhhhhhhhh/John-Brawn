using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR 

[CustomEditor(typeof(GridController))]
public class GridControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridController script = (GridController)target;

        if (GUILayout.Button("Draw Map"))
        {
            script.initLayers();
            script.instantiateGrid();
        }

        if (GUILayout.Button("Erase Map"))
        {
            script.deleteGrid();
        }
    }
}

#endif
