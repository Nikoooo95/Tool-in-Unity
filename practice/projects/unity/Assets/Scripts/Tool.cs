using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

/// <summary>
/// DLL Wrapper. Contains the basics functions and comunicates with the DLL.
/// It calls to some methods of C++ and receives stuff from the DLL.
/// </summary>
public unsafe class Tool
{
    #region Native
    struct NativeTool { }

    ///ALL THE METHODS THAT FOLLOW ARE WRITTEN IN CAMELCASE BECAUSE ARE C++ METHODS
    ///I KNOW THAT METHODS IN C# MUST BE IN PASCALCASE, BUT I THINK THAT SHOULD BE BETTER
    ///TO KEEP THE METHODS OF C++ IN CAMELCASE INSTEAD OF MAKE THESE METHODS IN PASCALCASE 

    /// <summary>
    /// Calls the constructor of the C++ Tool
    /// </summary>
    /// <returns>Pointer to the C++ Tool</returns>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern NativeTool* createTool();

    /// <summary>
    /// Parses the .gonic file
    /// </summary>
    /// <param name="tool">Pointer to the C++ tool</param>
    /// <param name="path">Path to the .gonic file</param>
    /// <returns>True if the parse was good. False if was not.</returns>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool parseFile(NativeTool* tool, string path);

    /// <summary>
    /// Gets the amount of 2D Layers in the .gonic file
    /// </summary>
    /// <param name="ptr">Pointer to the c++ tool</param>
    /// <returns>Amount of 2D Layers</returns>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getLayers2dAmount(NativeTool* ptr);

    /// <summary>
    /// Gets the name of a concrete layer
    /// </summary>
    /// <param name="ptr">Pointer to the c++ tool</param>
    /// <param name="layer">Number of the concrete layer</param>
    /// <returns>The name of the layer in IntPtr format</returns>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getLayer2dName(NativeTool* ptr, int layer);

    /// <summary>
    /// Gets the amount of 2D models in a concrete 2D layer of the .gonic file
    /// </summary>
    /// <param name="ptr">Pointer to the c++ tool</param>
    /// <param name="layer">Number of the layer</param>
    /// <returns>Amount of 2D models in the concrete layer of the -gonic file</returns>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getModelsInLayer2dAmount(NativeTool* ptr, int layer);

    /// <summary>
    /// Gets the name of a 2D model of a 2d layer
    /// </summary>
    /// <param name="ptr">Pointer to the c++ tool</param>
    /// <param name="layer">Number of the layer</param>
    /// <param name="model">Number of the model</param>
    /// <returns>The name of the model in IntPtr format</returns>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getModelNameInLayer(NativeTool* ptr, int layer, int model);

    /// <summary>
    /// Gets the amount of 2D Vectors of a concrete 2D model
    /// </summary>
    /// <param name="ptr">Pointer to the c++ tool</param>
    /// <param name="layer">Number of the layer</param>
    /// <param name="model">Number of the model</param>
    /// <returns>Amount of 2D vectors of a 2D model</returns>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getVectorsAmount(NativeTool* ptr, int layer, int model);

    /// <summary>
    /// Fills an array of 2D Vector with the Vertex positions of a 2D model
    /// </summary>
    /// <param name="ptr">Pointer to the c++ tool</param>
    /// <param name="layer">Number of the layer</param>
    /// <param name="model">Number of the model</param>
    /// <param name="vectors">Array of Vector2 to fill</param>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void fillVectors2d(NativeTool* ptr, int layer, int model, Vector2[] vectors);

    /// <summary>
    /// Fills an array of 3D Vector with the Vertex positions of a 3D model
    /// </summary>
    /// <param name="ptr">Pointer to the C++ tool</param>
    /// <param name="layer">Number of the layer</param>
    /// <param name="model">Number of the model</param>
    /// <param name="vectors">Array of Vector3 to fill</param>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void fillVectors3d(NativeTool* ptr, int layer, int model, Vector3[] vectors);

    /// <summary>
    /// Gets the color of a model
    /// </summary>
    /// <param name="ptr">Pointer to the C++ tool</param>
    /// <param name="layer">Number of the layer</param>
    /// <param name="model">Number of the model</param>
    /// <param name="color">Pointer to a Color</param>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void getColor(NativeTool* ptr, int layer, int model, Color* color);

    /// <summary>
    /// Generates a 3D layer from a 2D layer
    /// </summary>
    /// <param name="ptr">Pointer to the C++ tool</param>
    /// <param name="layer">Layer 2D which is goint to convert to a 3D layer</param>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void generateLayer3d(NativeTool* ptr, int layer);

    /// <summary>
    /// Transform a 2D model to a 3D model from a Layer
    /// </summary>
    /// <param name="ptr">Pointer to the C++ tool</param>
    /// <param name="layer">3D Layer where the 3D model is goint to be stored</param>
    /// <param name="model">2D model we want to transform to 3D</param>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void transform2dTo3d(NativeTool* ptr, int layer, int model);

    /// <summary>
    /// Generates the index positions for triangles of a 3D model
    /// </summary>
    /// <param name="ptr">Pointer to the C++ tool</param>
    /// <param name="triangles">Array of ints to storage the index positions</param>
    /// <param name="amount">Amount of index positions will have the 3D model</param>
    /// <param name="backFaces">True if has to be generated with backfaces. False if is not</param>
    /// <param name="looped">True if has to be generated with an extra wall to close the shape. False if is not.</param>
    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void generateTriangles(NativeTool* ptr, int[] triangles, int amount, bool backFaces, bool looped);

    #endregion
    
    /// <summary>
    /// Pointer to the native Tool in C++
    /// </summary>
    NativeTool* nativePointer;


    #region API friendly

    /// <summary>
    /// Constructor of the C# Tool. Calls the constructor of the C++ Tool
    /// and parses the .gonic file.
    /// </summary>
    /// <param name="path">Path to the .gonic file</param>
    public Tool(string path)
    {

        nativePointer = createTool();

        Debug.Log("Parsing...");
        var ptr = parseFile(nativePointer, path);
        Debug.Log("Parsed.");
    }

    /// <summary>
    /// Load all the 2D stuff of the .gonic file already parsed
    /// </summary>
    /// <param name="looped">True if the 2D models have to be looped</param>
    public void Load2D(bool looped)
    {
        int layersAmount = getLayers2dAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            ///Creates an objet per layer
            GameObject layer = new GameObject();
            layer.name = GetNameFromLayer(i);
            layer.tag = "Layer";
            int modelsAmount = getModelsInLayer2dAmount(nativePointer, i);
            for (int j = 0; j < modelsAmount; ++j)
            {
                ///Creates an object per model and makes child of the layer
                GameObject model = new GameObject();
                model.name = GetNameFromModel(i, j);
                model.transform.parent = layer.transform;

                ///Gets the color of the 2D model of C++
                Color col = Color.black;
                Color* color = &col;
                getColor(nativePointer, i, j, color);

                ///Generates the Line Render
                GenerateLineRenderer(model, GetPositions3D(i, j), looped, color);
            }
        }
    }

    /// <summary>
    /// Load all the 3D stuff transforming the 2D objects into 3D objects
    /// </summary>
    /// <param name="looped">True if the 3D models have to be looped</param>
    /// <param name="keepLineRenderer">True if the line renderers of 2D models continue enabled</param>
    /// <param name="backFaces">True if the back faces of the model are visible.</param>
    public void Load3D(bool looped, bool keepLineRenderer, bool backFaces)
    {
        int layersAmount = getLayers2dAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            ///Gets the layer previously created
            GameObject layer = GameObject.Find(GetNameFromLayer(i));
            generateLayer3d(nativePointer, i);
            int modelsAmount = getModelsInLayer2dAmount(nativePointer, i);

            for (int j = 0; j < modelsAmount; ++j)
            {
                ///Gets the gameOject of the model previously created
                GameObject model = GameObject.Find(GetNameFromModel(i, j));

                ///Generates the new 3D model
                transform2dTo3d(nativePointer, i, j);

                ///Add a MeshFilter
                if (model.GetComponent<MeshFilter>() == null)
                    model.AddComponent<MeshFilter>();

                ///Add a Mesh Renderer
                if (model.GetComponent<MeshRenderer>() == null)
                    model.AddComponent<MeshRenderer>();

                ///Enable / Disable Line Renderer of the Model gameObject
                if (!keepLineRenderer)
                    model.GetComponent<LineRenderer>().enabled = false;
                else
                    model.GetComponent<LineRenderer>().enabled = true;

                ///Set the loop of the LineRenderer
                model.GetComponent<LineRenderer>().loop = looped;

                ///Sets the Mesh properties
                Mesh mesh = new Mesh();
                mesh.Clear();

                ///Vertex
                Vector3[] vertices = new Vector3[getVectorsAmount(nativePointer, i, j) * 2];
                fillVectors3d(nativePointer, i, j, vertices);

                mesh.vertices = vertices;

                ///Triangles
                int[] triangles;
                if(!looped)
                {
                   triangles = GenerateTriangles(getVectorsAmount(nativePointer, i, j) * 2 - 2, backFaces, looped);
                }
                else
                {
                    triangles = GenerateTriangles(getVectorsAmount(nativePointer, i, j) * 2 - 2, backFaces, looped);
                }
                mesh.triangles = triangles;

                ///Sets the mesh into the GameObject
                model.GetComponent<MeshFilter>().mesh = mesh;

                ///Sets the color and the Material
                Color col = Color.black;
                Color* color = &col;
                getColor(nativePointer, i, j, color);
                Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Color"));
                whiteDiffuseMat.SetColor("_Color", *color);
                model.GetComponent<MeshRenderer>().material = whiteDiffuseMat;
            }
        }
    }

    /// <summary>
    /// Generates the Triangles for a new 3D model
    /// </summary>
    /// <param name="trianglesAmount">The amount of triangles the Model needs</param>
    /// <param name="backFaces">True if the backFaces have to be generated</param>
    /// <param name="looped">True if is looped and have to calculate the end of the shape</param>
    /// <returns>Array of int which contains the drawn order of the triangles</returns>
    int[] GenerateTriangles(int trianglesAmount, bool backFaces, bool looped)
    {
        int[] triangles;
        ///If is looped, has to add 2 vertex more
        if (looped)
            trianglesAmount += 2;

        ///Here it is calculated the real number of the triangles will have the model
        if (backFaces)
            trianglesAmount *= 6;
        else
            trianglesAmount *= 3;

        triangles = new int[trianglesAmount];

        ///The triangles are calculated in the dll
        generateTriangles(nativePointer, triangles, trianglesAmount, backFaces, looped);

        return triangles;
    }

    /// <summary>
    /// Generates the positions of the LineRenderer
    /// </summary>
    /// <param name="model">GameObject which contains the Line Renderer</param>
    /// <param name="positions">Array of Vector3 with the positions of the Line Renderer</param>
    /// <param name="looped">True if the Line Renderer has to be looped</param>
    /// <param name="color">Color of the Line Renderer</param>
    void GenerateLineRenderer(GameObject model, Vector3[] positions, bool looped, Color* color)
    {
        LineRenderer line = model.AddComponent<LineRenderer>();
        line.positionCount = positions.Length;

        ///Add the positions
        line.SetPositions(positions);
        line.startWidth = 0.25f;
        line.endWidth = 0.25f;

        ///Add the color
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Color"));
        whiteDiffuseMat.SetColor("_Color", *color);
        line.material = whiteDiffuseMat;

        ///Makes the loop
        line.loop = looped;
    }

    /// <summary>
    /// Generates the 3D positions from a 2D model for its visualization
    /// </summary>
    /// <param name="layer">Layer of the 2D model</param>
    /// <param name="model">2D Model which contains its coordinates</param>
    /// <returns>Array of Vector3 with the 3D positions for the Line Renderer</returns>
    Vector3[] GetPositions3D(int layer, int model)
    {
        Vector2[] positions2D = new Vector2[getVectorsAmount(nativePointer, layer, model)];
        fillVectors2d(nativePointer, layer, model, positions2D);
        Vector3[] positions3D = new Vector3[positions2D.Length];
        ConvertVector(positions3D, positions2D);
        return positions3D;
    }

    /// <summary>
    /// Gets the name of a 2D layer
    /// </summary>
    /// <param name="layer">2D Layer</param>
    /// <returns>Name of the layer</returns>
    string GetNameFromLayer(int layer)
    {
        var ptr = getLayer2dName(nativePointer, layer);
        return Marshal.PtrToStringAnsi(ptr);
    }

    /// <summary>
    /// Gets the name of the 2D model of a Layer
    /// </summary>
    /// <param name="layer">2D Layer</param>
    /// <param name="model">2D Model</param>
    /// <returns>Name of the 2D Model</returns>
    string GetNameFromModel(int layer, int model)
    {
        var ptr = getModelNameInLayer(nativePointer, layer, model);
        return Marshal.PtrToStringAnsi(ptr);
    }

    /// <summary>
    /// Transforms an array of 2D Vector into an array of 3D vector
    /// </summary>
    /// <param name="vec3">Array of Vector 3</param>
    /// <param name="vec2">Array of Vector 2</param>
    void ConvertVector(Vector3[] vec3, Vector2[] vec2)
    {
        for (int i = 0; i < vec3.Length; ++i)
        {
            vec3[i] = new Vector3(vec2[i].x, 0.0f, vec2[i].y);
        }
    }

    /// <summary>
    /// Deletes all the Layers and Models from the Scene
    /// </summary>
    public void Clean()
    {
        GameObject [] gameObjects = GameObject.FindGameObjectsWithTag("Layer");
        foreach(GameObject gameObject in gameObjects)
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
    }

    /// <summary>
    /// Saves all the 3D model generated by the tool as a Prefab
    /// </summary>
    /// <param name="savePath"></param>
    public void SavePrefab3D(string savePath)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Layer");
        foreach (GameObject gameObject in gameObjects)
        {
            for (int i = 0; i < gameObject.transform.childCount; ++i)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;

                ///Saves the Material from the Liner Renderer
                if (child.GetComponent<LineRenderer>() != null)
                    SaveMaterial(child.GetComponent<LineRenderer>().sharedMaterial, "LineRenderer" + child.name);

                ///Saves the Material from the Mesh
                SaveMaterial(child.GetComponent<MeshRenderer>().sharedMaterial, "Mesh"+child.name);
                ///Saves the Mesh
                SaveMesh(child.GetComponent<MeshFilter>().sharedMesh, child.name);
            }

            ///Saves the Prefab
            SaveAsPrefab(gameObject, savePath);
        }

    }

    /// <summary>
    /// Saves the Material
    /// </summary>
    /// <param name="mat">Material</param>
    /// <param name="name">Name of the Material</param>
    private void SaveMaterial(Material mat, string name)
    {
        ///First checks the path. Create it if it does not exist.
        if (!Directory.Exists("Assets/Materials"))
        {
            Directory.CreateDirectory("Assets/Materials");
        }

        if (!AssetDatabase.LoadAssetAtPath("Assets/Materials/" + name + ".mat", typeof(Material)))
        {
            AssetDatabase.CreateAsset(mat, "Assets/Materials/" + name + ".mat");

        }
    }

    /// <summary>
    /// Saves the Mesh
    /// </summary>
    /// <param name="mesh">Mesh</param>
    /// <param name="name">Name of the Mesh</param>
    private void SaveMesh(Mesh mesh, string name)
    {
        ///First checks the path. Create it if it does not exist.
        if (!Directory.Exists("Assets/Meshes"))
        {
            Directory.CreateDirectory("Assets/Meshes");
        }

        if (!AssetDatabase.LoadAssetAtPath("Assets/Meshes/" + name + ".mesh", typeof(Mesh)))
        {
            AssetDatabase.CreateAsset(mesh, "Assets/Meshes/" + name + ".mesh");
            
        }
     

    }

    /// <summary>
    /// Saves a GameObject as a Prefab in a concrete path
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="savePath">The path</param>
    private void SaveAsPrefab(GameObject gameObject, string path)
    {

        ///First, checks the path. Creates it if it does not exist.
        if (!Directory.Exists(path))
        {
            if (EditorUtility.DisplayDialog("Warning",
                "The directory doesn't exist. Do you want to create it?",
                "Yes",
                "No"))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                return;
            }
        }

        ///Saves the prefab in the path
        if (AssetDatabase.LoadAssetAtPath(path + gameObject.name + ".prefab", typeof(GameObject)))
        {
            ///Checks if a prefab with the same name exists in that path
            if (EditorUtility.DisplayDialog("Are you sure?",
                "The Prefab already exists in the path " + path + gameObject.name + ".prefab. Do you want to overwrite it?",
                "Yes",
                "No"))

            {
                CreatePrefab(path + gameObject.name + ".prefab", gameObject);
            }
        }
        else
        {
            CreatePrefab(path + gameObject.name + ".prefab", gameObject);
        }
    }
   
    /// <summary>
    /// Creates a Prefab from a GameObject and saves it in a concrete Path
    /// </summary>
    /// <param name="savePath"></param>
    /// <param name="gameObject"></param>
    void CreatePrefab(string savePath, GameObject gameObject)
    {
        Debug.Log("Saved!");
        UnityEngine.Object prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, savePath);
    }

    #endregion
}
