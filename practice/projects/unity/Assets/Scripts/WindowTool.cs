using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;

public class WindowTool : EditorWindow
{
    /// <summary>
    /// The path where the .gonic file is stored.
    /// </summary>
    string loadPath = "Assets\\Gonic\\file.gonic";

    /// <summary>
    /// The path where the 3D prefab will be stored.
    /// </summary>
    string savePath = "Assets/Prefabs/";

    /// <summary>
    /// The optional settings. True if are enabled.
    /// </summary>
    bool optionalSettings;

    /// <summary>
    /// True if the .gonic file has been parsed.
    /// </summary>
    bool parsed = false;

    /// <summary>
    /// True if the 2D model has been generated.
    /// </summary>
    bool generated2D = false;

    /// <summary>
    /// True if the 3D model has been generated.
    /// </summary>
    bool generated3D = false;

    /// <summary>
    /// True if the user wants to have the back faces.
    /// </summary>
    bool backFaces = false;

    /// <summary>
    /// True if the user wants to have the shapes looped.
    /// </summary>
    bool looped = false;

    /// <summary>
    /// True if the user wants to keep visible the Line Renderers
    /// </summary>
    bool keepLineRenderer = true;
	
    /// <summary>
    /// The tool in C#
    /// </summary>
    Tool tool;

    /// <summary>
    /// Determinates where the user can open the Tool and shows the window
    /// </summary>
    [MenuItem("Window/Tools/Gonic Tool")]
    static void Init()
    {
        WindowTool window = (WindowTool)EditorWindow.GetWindow(typeof(WindowTool));
        window.Show();
    }

    /// <summary>
    /// Shows all the UI of the Tool for the user
    /// </summary>
    private void OnGUI()
    {
        ///Base Settings - Path
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        loadPath = EditorGUILayout.TextField("Path", loadPath);

        EditorGUILayout.Space();

        ///Optional Settings - Looped | Keep Line Render | Back Faces
        optionalSettings = EditorGUILayout.BeginToggleGroup("Optional Settings", optionalSettings);

        looped = EditorGUILayout.Toggle("Loop ", looped);
        keepLineRenderer = EditorGUILayout.Toggle("Keep Line Renderer in 3D", keepLineRenderer);
        backFaces = EditorGUILayout.Toggle("Show back faces", backFaces);

        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        ///Space where the user can Parse the .gonic file
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Parse / Delete", EditorStyles.boldLabel);
        if (GUILayout.Button("Read .gonic File"))
        {
            if (!CheckPath())
            {
                parsed = false;
                return;
            }

            ///Creates the Tool and start to parse the file
            EditorUtility.DisplayProgressBar("Tool", "Parsing...", 0);
            tool = new Tool(loadPath);
            EditorUtility.DisplayProgressBar("Tool", "Parsing...", 100);
            EditorUtility.ClearProgressBar();
            parsed = true;
        }
        EditorGUILayout.EndVertical();
        
        ///To delete all the .gonic files of the Unity Scene that have been generated
        EditorGUI.BeginDisabledGroup(!parsed);
        if (GUILayout.Button("Clean All From Scene"))
        {
            if (EditorUtility.DisplayDialog("Are you sure?",
                    "This will delete all the Layers and Meshes created.",
                    "Yes",
                    "No"))
            {
                if(tool != null)
                {
                    tool.Clean();
                }
                
                parsed = false;
                generated2D = false;
                generated3D = false;
            }
               
        }
        EditorGUI.EndDisabledGroup();



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        ///2D Space to generate basics forms from the .gonic file
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
        EditorGUILayout.EndVertical();




        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();



        ///3D Space  to generate the models from the 2D models
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("3D", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(!generated2D);

        if (GUILayout.Button("Load 3D"))
        {
            tool.Load3D(looped, keepLineRenderer, backFaces);
            generated3D = true;
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();



        ///Space to save all the .gonic stuff as Prefab. Also saves the Meshes and the Materials
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Save", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(!generated3D);
        savePath = EditorGUILayout.TextField("Path", savePath);
        if (GUILayout.Button("Save as Prefab"))
        {
            tool.SavePrefab3D(savePath);

        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();

    }

    /// <summary>
    /// Checks if the load Path exists
    /// </summary>
    /// <returns></returns>
    private bool CheckPath()
    {
        if (!System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\" + loadPath))
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

    /// <summary>
    /// Creates a .gonic file empty
    /// </summary>
    /// <param name="loadPath"></param>
    /// <returns></returns>
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

