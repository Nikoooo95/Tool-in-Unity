using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;

public class WindowTool : EditorWindow
{


    Exporter exporterTool;


    bool optionalSettings;
    bool looped = false;

    [MenuItem("Window/Tools/OBJ Expoter")]
    static void Init()
    {
        WindowTool tool = (WindowTool)EditorWindow.GetWindow(typeof(WindowTool));
        tool.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        optionalSettings = EditorGUILayout.BeginToggleGroup("Optional Settings", optionalSettings);
        looped = EditorGUILayout.Toggle("Loop ", looped);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Buttons");

        if (GUILayout.Button("Export"))
        {
            Debug.Log("Exportameame esta cruck");
        }
    }
}
