using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;

public class WindowTool : EditorWindow
{
    string loadPath = System.IO.Directory.GetCurrentDirectory() + "\\Assets\\Gonic\\file.gonic";
    string savePath = "Assets/Prefabs/";
    bool optionalSettings;


    private GameObject[] layers;
    bool parsed = false;
    bool generated2D = false;
    bool generated3D = false;
    bool backFaces = false;
    bool looped = false;
    bool keepLineRenderer = false;
	
    Tool tool;


    [MenuItem("Tools/My New Tool")]
    static void Init()
    {
        WindowTool window = (WindowTool)EditorWindow.GetWindow(typeof(WindowTool));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        loadPath = EditorGUILayout.TextField("Path", loadPath);

        optionalSettings = EditorGUILayout.BeginToggleGroup("Optional Settings", optionalSettings);

        looped = EditorGUILayout.Toggle("Loop ", looped);
        keepLineRenderer = EditorGUILayout.Toggle("Keep Line Renderer in 3D", keepLineRenderer);
        backFaces = EditorGUILayout.Toggle("Show back faces", backFaces);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Buttons");

        if (GUILayout.Button("Read File"))
        {

            if (!CheckPath())
            {
                parsed = false;
                return;
            }
                

            EditorUtility.DisplayProgressBar("Tool", "Parsing...", 0);
            tool = new Tool(loadPath);
            EditorUtility.DisplayProgressBar("Tool", "Parsing...", 100);
            EditorUtility.ClearProgressBar();
            parsed = true;

        }

        

        EditorGUI.BeginDisabledGroup(!parsed);
        if (GUILayout.Button("Clean All From Scene"))
        {
            if (EditorUtility.DisplayDialog("Are you sure?",
                    "This will delete all the Layers and Meshes created.",
                    "Yes",
                    "No"))
            {
                tool.Clean();
                parsed = false;
                generated2D = false;
                generated3D = false;
            }
               
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        GUILayout.Label("2D", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(!parsed);

        if (GUILayout.Button("Load 2D"))
        {
            tool.Clean();
            tool.Load2D(looped);
            generated2D = true;
        }

        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(!generated2D);
        savePath = EditorGUILayout.TextField("Path", savePath);
        if (GUILayout.Button("Save as Prefab"))
        {
            tool.SaveAsPrefab2D(savePath);
           
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        GUILayout.Label("3D", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(!generated2D);

        if (GUILayout.Button("Load 3D"))
        {
            tool.Load3D(looped, keepLineRenderer, backFaces);
            generated3D = true;
        }

        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(!generated3D);
        savePath = EditorGUILayout.TextField("Path", savePath);
        if (GUILayout.Button("Save as Prefab"))
        {
           // tool.SaveAsPrefab2D(savePath);

        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();



    }

    private bool CheckPath()
    {
        if (!System.IO.File.Exists(loadPath))
        {
            if (EditorUtility.DisplayDialog("Warning",
                    "The directory is not valid. Do you want to create it?",
                    "Yes",
                    "No"))
            {
                return CreateFile(loadPath);
            }
            else
            {
                return false;
            }

        }
        else
        {
            if (System.IO.Path.GetExtension(loadPath) != ".gonic")
                return false;
        }
        return true;
    }

    private bool CreateFile(string loadPath)
    {
        if (System.IO.Path.GetExtension(loadPath) == ".gonic")
        {
            System.IO.File.WriteAllText(loadPath, "<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n<gonic>\n\n</gonic>");
            return true;
        }
        else
        {
            if (EditorUtility.DisplayDialog("Warning",
            "The file is not a .gonic file. Do you prefer to create a .gonic file?",
            "Yes",
            "No"))
            {
                loadPath = System.IO.Path.ChangeExtension(loadPath, ".gonic");
                System.IO.File.WriteAllText(loadPath, "<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n<gonic>\n\n</gonic>");
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}

