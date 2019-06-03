using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
public class WindowTool : EditorWindow
{

    string path = "D:\\Usuarios\\Nicolas\\Documentos\\Tool-in-Unity\\practice\\projects\\unity\\Assets\\Plugins\\file\\file.xml";

    /*bool optionalSettings;

    Color color;
    float size = 1;*/

    bool parsed = false;

    Tool tool;

    [MenuItem("Tool/Room Generator")]
    static void Init()
    {
        WindowTool tool = (WindowTool)EditorWindow.GetWindow(typeof(WindowTool));
        tool.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        path = EditorGUILayout.TextField("Path", path);

        /*optionalSettings = EditorGUILayout.BeginToggleGroup("Optional Settings", optionalSettings);
        color = EditorGUILayout.ColorField("Color", color);
        size = EditorGUILayout.Slider("Size", size, 0.0f, 5.0f);
        EditorGUILayout.EndToggleGroup();*/

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Buttons");

        if(GUILayout.Button("Read File"))
        {
            EditorUtility.DisplayProgressBar("CosoTool", "Parseando.....", 0);
            tool = new Tool(path);
            EditorUtility.DisplayProgressBar("CosoTool", "Parseando.....", 100);
            EditorUtility.ClearProgressBar();
            parsed = true;
           
        }

        EditorGUI.BeginDisabledGroup(!parsed);
        if (GUILayout.Button("Load 2D"))
        {
            tool.Load2D();
        }
        EditorGUI.EndDisabledGroup();
    }

}

