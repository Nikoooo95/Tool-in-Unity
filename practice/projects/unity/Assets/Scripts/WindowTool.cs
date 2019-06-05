using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System;

public class WindowTool : EditorWindow
{
    Exporter exporterTool;
    struct Settings
    {
        public string path;
        public string name;
        public bool allScene;
    };

    static Settings toolSettings;

    private int objectsCount;

    [MenuItem("Window/Tools/OBJ Expoter")]
    static void Init()
    {
        WindowTool tool = (WindowTool)EditorWindow.GetWindow(typeof(WindowTool));
        tool.Show();
        toolSettings.allScene = false;
        toolSettings.name = "scene";
        toolSettings.path = "D:/UNIVERSIDAD/Unity/Tool-in-Unity/practice/projects/unity/Assets/";

        
    }

    private void OnGUI()
    {

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        toolSettings.path = EditorGUILayout.TextField("File path", toolSettings.path);
        toolSettings.name = EditorGUILayout.TextField("File name", toolSettings.name);
        toolSettings.allScene = EditorGUILayout.Toggle("Export all scene", toolSettings.allScene);
        
        EditorGUILayout.Space();
        GUILayout.Label("Selected Objects " + GetSelected(), EditorStyles.label);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Export"))
        {
            if (GetSelected() == 0)
            {
                EditorUtility.DisplayDialog("ERROR", "It have to be selected almost one object to export. " +
                            "You can check 'Export all scene' if you want to export all the meshes on the current scene", "Close");

                return;
            }


            if (CheckPath() && CheckName())
            {
                exporterTool = new Exporter();

                exporterTool.allScene = toolSettings.allScene;
                exporterTool.name = toolSettings.name;
                if (!exporterTool.Export(toolSettings.path, toolSettings.name))
                {
                    Debug.Log("Error: " + exporterTool.GetLog());
                    EditorUtility.DisplayDialog("ERROR", "Export error: " + exporterTool.GetLog(), "Close");
                }
            }
        }
    }

    private int GetSelected()
    {
        int count = 0;
        if(!toolSettings.allScene)
        {
            foreach(GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<MeshFilter>() != null)
                    ++count;
            }
        }
        else
        {
            count = (FindObjectsOfType(typeof(MeshFilter))).Length;
        }

        return count;
    }

    private bool CheckPath()
    {
        if(toolSettings.path.Length == 0)
        {
            EditorUtility.DisplayDialog("ERROR", "The path field can not be empty.", "Close");
            return false;
        }

        if(!System.IO.Directory.Exists(toolSettings.path))
        {
            if(EditorUtility.DisplayDialog("ERROR", "The current directory is not valid", "Create directory", "Close"))
            {
                System.IO.Directory.CreateDirectory(toolSettings.path);
                return true;
            }
            else
            {
                return false;
            }
        }

        if(toolSettings.path[toolSettings.path.Length -1] != '/')
        {
            EditorUtility.DisplayDialog("ERROR", "The directory has to end to the character '/'. Please check it and try again.", "Close");
            return false;
        }

        return true;
    }

    private bool CheckName()
    {
        if(toolSettings.name.Length == 0)
        {
            EditorUtility.DisplayDialog("ERROR", "The name field can not be empty.", "Close");
            return false;
        }

        if(System.IO.File.Exists(toolSettings.path + toolSettings.name + ".obj"))
        {
            EditorUtility.DisplayDialog("ERROR", "There is already an object with that name in the directory. Please, delete the file or change the name", "Close");
            return false;
        }

        return true;
    }
}
