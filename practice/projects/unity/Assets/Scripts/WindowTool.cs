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
    bool generated = false;
    bool looped = false;
	
    Tool tool;


    [MenuItem("Tools/My New Tool")]
    static void Init()
    {
        WindowTool tool = (WindowTool)EditorWindow.GetWindow(typeof(WindowTool));
        tool.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        loadPath = EditorGUILayout.TextField("Path", loadPath);

        optionalSettings = EditorGUILayout.BeginToggleGroup("Optional Settings", optionalSettings);

        looped = EditorGUILayout.Toggle("Loop ", looped);
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

        if (GUILayout.Button("Load 2D"))
        {
            tool.Load2D(looped);
            generated = true;
        }

        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(!generated);
        if (GUILayout.Button("Clean All From Scene"))
        {
            if (EditorUtility.DisplayDialog("Are you sure?",
                    "This will delete all the Layers and Meshes created.",
                    "Yes",
                    "No"))
            {
                tool.Clean2D();
                parsed = false;
                generated = false;
            }
               
        }
        EditorGUI.EndDisabledGroup();



        EditorGUILayout.BeginVertical(EditorStyles.helpBox); //Declaring our first part of our layout, and adding a bit of flare with EditorStyles.

        GUILayout.Label("2D", EditorStyles.boldLabel); //Making a label in our vertical view, declaring its contents, and adding editor flare.
        savePath = EditorGUILayout.TextField("Path", savePath);

        EditorGUI.BeginDisabledGroup(!generated);
        if (GUILayout.Button("Save as Prefab"))
        {
            tool.SaveAsPrefab2D(savePath);
           
        }
        EditorGUI.EndDisabledGroup();

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

