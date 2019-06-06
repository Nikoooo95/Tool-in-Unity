using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

//Wrapper
public unsafe class Tool
{
    #region Native
    struct NativeTool { }

    

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern NativeTool* createTool();

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool parseFile(NativeTool* tool, string path);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getLayers2dAmount(NativeTool* ptr);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getLayer2dName(NativeTool* ptr, int layer);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getModelsInLayer2dAmount(NativeTool* ptr, int layer);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getModelNameInLayer(NativeTool* ptr, int layer, int model);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getVectorsAmount(NativeTool* ptr, int layer, int model);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fillVectors2d(NativeTool* ptr, int layer, int model, Vector2[] vectors);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fillVectors3d(NativeTool* ptr, int layer, int model, Vector3[] vectors);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void getColor(NativeTool* ptr, int layer, int model, Color* color);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void generateLayer3d(NativeTool* ptr, int layer);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void transform2dTo3d(NativeTool* ptr, int layer, int model);
    

    #endregion

    NativeTool* nativePointer;
    Material material2D;

    #region API friendly
    public Tool(string path){

        nativePointer = createTool();
        Debug.Log("Parsing...");
        var ptr = parseFile(nativePointer, path);
        Debug.Log("Parsed.");
        material2D = Resources.Load("Line2D.mat", typeof(Material)) as Material;
    }

    public void Load2D(bool looped)
    {
        int layersAmount = getLayers2dAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            GameObject layer = new GameObject();
            layer.name = GetNameFromLayer(i);
            layer.tag = "Layer";
            int modelsAmount = getModelsInLayer2dAmount(nativePointer, i);
            for (int j = 0; j < modelsAmount; ++j)
            {
                GameObject model = new GameObject();
                model.name = GetNameFromModel(i, j);
                model.transform.parent = layer.transform;

                Color col = Color.black;
                Color* color = &col;
                getColor(nativePointer, i, j, color);
                GenerateLineRenderer(model, GetPositions3D(i, j), looped, color);
            }
        }
    }

    public void Load3D(bool looped, bool keepLineRenderer, bool backFaces)
    {
        int layersAmount = getLayers2dAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            GameObject layer = GameObject.Find(GetNameFromLayer(i));
            generateLayer3d(nativePointer, i);
            int modelsAmount = getModelsInLayer2dAmount(nativePointer, i);
            for (int j = 0; j < modelsAmount; ++j)
            {
                GameObject model = GameObject.Find(GetNameFromModel(i, j));

                Color col = Color.black;
                Color* color = &col;
                getColor(nativePointer, i, j, color);
                transform2dTo3d(nativePointer, i, j);
                if(model.GetComponent<MeshFilter>() == null)
                    model.AddComponent<MeshFilter>();

                if (model.GetComponent<MeshRenderer>() == null)
                    model.AddComponent<MeshRenderer>();

                if(!keepLineRenderer)
                    model.GetComponent<LineRenderer>().enabled = false;
                else
                    model.GetComponent<LineRenderer>().enabled = true;

                model.GetComponent<LineRenderer>().loop = looped;
                Mesh mesh = new Mesh();
                mesh.Clear();
                Vector3[] vertices = new Vector3[getVectorsAmount(nativePointer, i, j) * 2];
                fillVectors3d(nativePointer, i, j, vertices);

                mesh.vertices = vertices;

                int[] triangles;
                if(!looped)
                {
                   triangles = GenerateTriangles(getVectorsAmount(nativePointer, i, j) * 2 - 2, backFaces, looped);
                }
                else
                {
                    triangles = GenerateTriangles(getVectorsAmount(nativePointer, i, j) * 2 - 2, backFaces, looped);
                }
                
                //Debug.Log(triangles.Length);
                mesh.triangles = triangles;
                model.GetComponent<MeshFilter>().mesh = mesh;
                // Debug.Log(col);

                Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Color"));
                whiteDiffuseMat.SetColor("_Color", *color);
                model.GetComponent<MeshRenderer>().material = whiteDiffuseMat;
            }
        }
    }

    int[] GenerateTriangles(int trianglesAmount, bool backFaces, bool looped)
    {
        int[] triangles;// = new int[trianglesAmount * 3];
        if (looped)
            trianglesAmount += 2;

        if (backFaces)
            trianglesAmount *= 6;
        else
            trianglesAmount *= 3;


        triangles = new int[trianglesAmount];

        if (looped)
            trianglesAmount -= 2;

        for (int i = 0, j = 0; i < trianglesAmount; i+=6, j+=2)
        {
            triangles[i] = j;           //
            triangles[i + 1] = j + 2;
            triangles[i + 2] = j + 1;

            triangles[i + 3] = j + 1;
            triangles[i + 4] = j + 2;
            triangles[i + 5] = j + 3;
            if (backFaces)
            {
                triangles[i + 6] = j + 1;
                triangles[i + 7] = j + 2;
                triangles[i + 8] = j;

                triangles[i + 9] = j + 1;
                triangles[i + 10] = j + 3;
                triangles[i + 11] = j + 2;
                i += 6;

            }
        }

        if(looped)
        {
            if (!backFaces)
            {

                triangles[triangles.Length - 6] = triangles[triangles.Length - 8];
                triangles[triangles.Length - 5] = triangles[0];
                triangles[triangles.Length - 4] = triangles[triangles.Length - 7];

                triangles[triangles.Length - 3] = triangles[triangles.Length - 7];
                triangles[triangles.Length - 2] = triangles[0];
                triangles[triangles.Length - 1] = triangles[2];
            }
            else
            {
                triangles[triangles.Length - 12] = triangles[triangles.Length - 20];  //10
                triangles[triangles.Length - 11] = triangles[0];                      //0
                triangles[triangles.Length - 10] = triangles[triangles.Length - 19]; //11

                triangles[triangles.Length - 9] = triangles[triangles.Length - 19];  //11
                triangles[triangles.Length - 8] = triangles[0];                     //0
                triangles[triangles.Length - 7] = triangles[2];                         //1

                triangles[triangles.Length - 6] = triangles[triangles.Length - 9]; //
                triangles[triangles.Length - 5] = triangles[0];
                triangles[triangles.Length - 4] = triangles[triangles.Length - 12];

                triangles[triangles.Length - 3] = triangles[triangles.Length - 9];
                triangles[triangles.Length - 2] = triangles[2];
                triangles[triangles.Length - 1] = triangles[0];
            }
        }
            



        return triangles;

    }

    void GenerateLineRenderer(GameObject model, Vector3[] positions, bool looped, Color* color)
    {
        LineRenderer line = model.AddComponent<LineRenderer>();
        line.positionCount = positions.Length;
        line.SetPositions(positions);
        line.startWidth = 0.25f;
        line.endWidth = 0.25f;
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Color"));
        whiteDiffuseMat.SetColor("_Color", *color);
        line.material = whiteDiffuseMat;
        line.loop = looped;
    }

    Vector3[] GetPositions3D(int layer, int model)
    {
        Vector2[] positions2D = new Vector2[getVectorsAmount(nativePointer, layer, model)];
        fillVectors2d(nativePointer, layer, model, positions2D);
        Vector3[] positions3D = new Vector3[positions2D.Length];
        ConvertVector(positions3D, positions2D);
        return positions3D;
    }

    string GetNameFromLayer(int layer)
    {
        var ptr = getLayer2dName(nativePointer, layer);
        return Marshal.PtrToStringAnsi(ptr);
    }

    string GetNameFromModel(int layer, int model)
    {
        var ptr = getModelNameInLayer(nativePointer, layer, model);
        return Marshal.PtrToStringAnsi(ptr);
    }

    void ConvertVector(Vector3[] vec3, Vector2[] vec2)
    {
        for (int i = 0; i < vec3.Length; ++i)
        {
            vec3[i] = new Vector3(vec2[i].x, 0.0f, vec2[i].y);
        }
    }

    public void Clean()
    {
        GameObject [] gameObjects = GameObject.FindGameObjectsWithTag("Layer");
        foreach(GameObject gameObject in gameObjects)
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
    }

    public void SaveAsPrefab2D(string savePath)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Layer");
        foreach (GameObject gameObject in gameObjects)
        {
            if (!Directory.Exists(savePath))
                if (EditorUtility.DisplayDialog("Warning",
                    "The directory doesn't exists. Do you want to create it?",
                    "Yes",
                    "No"))
                {
                    Directory.CreateDirectory(savePath);
                }
                else
                {
                    return;
                }

                    savePath +=  gameObject.name + ".prefab";
            if (AssetDatabase.LoadAssetAtPath(savePath, typeof(GameObject)))
            {

                if (EditorUtility.DisplayDialog("Are you sure?",
                    "The Prefab already exists in the path " + savePath + " . Do you want to overwrite it?",
                    "Yes",
                    "No"))

                {
                    CreatePrefab(savePath, gameObject);
                }
            }
            else
            {
                CreatePrefab(savePath, gameObject);
            }
        }
    }

    void CreatePrefab(string savePath, GameObject gameObject)
    {
        Debug.Log("Saved!");
        UnityEngine.Object prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, savePath);
    }

    #endregion
}
