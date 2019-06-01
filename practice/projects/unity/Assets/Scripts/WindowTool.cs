using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
public class WindowTool : EditorWindow
{

    string path = "D:\\Usuarios\\Nicolas\\Documentos\\Tool-in-Unity\\practice\\projects\\unity\\Assets\\Plugins\\file\\file.xml";

    bool optionalSettings;

    Color color;
    float size = 1;
    Vector2 positions;

    Tool tool;
    [MenuItem("Window/Room Generator")]
    static void Init()
    {
        WindowTool tool = (WindowTool)EditorWindow.GetWindow(typeof(WindowTool));
        tool.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        path = EditorGUILayout.TextField("Path", path);

        optionalSettings = EditorGUILayout.BeginToggleGroup("Optional Settings", optionalSettings);
        color = EditorGUILayout.ColorField("Color", color);
        size = EditorGUILayout.Slider("Size", size, 0.0f, 5.0f);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Ey");

        if(GUILayout.Button("Read File"))
        {
            tool = new Tool(path);
           
        }

        if (GUILayout.Button("Get Layer"))
        {
            
            Debug.Log(tool.getLayerName());
        }

        positions = EditorGUILayout.Vector2Field("Valores", positions);
    }
}

