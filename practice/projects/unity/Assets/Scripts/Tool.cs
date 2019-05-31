using UnityEngine;
using UnityEditor;

public class Tool : EditorWindow
{

    string path = "../file.maravilloso";
    bool optionalSettings;

    Color color;
    float size = 1;

    [MenuItem("Window/Room Generator")]
    static void Init()
    {
        Tool tool = (Tool)EditorWindow.GetWindow(typeof(Tool));
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

        if(GUILayout.Button("Read File"))
        {
            Model model = new Model(path);
        }
    }
}

