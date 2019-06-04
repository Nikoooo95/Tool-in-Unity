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
    private static extern int getLayersAmount(NativeTool* ptr);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getLayerName(NativeTool* ptr, int layer);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getModelsInLayerAmount(NativeTool* ptr, int layer);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getModelNameInLayer(NativeTool* ptr, int layer, int model);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int getVectorsAmount(NativeTool* ptr, int layer, int model);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fillVectors(NativeTool* ptr, int layer, int model, Vector2[] vectors);

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
        int layersAmount = getLayersAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            GameObject layer = new GameObject();
            layer.name = GetNameFromLayer(i);
            layer.tag = "Layer";
            int modelsAmount = getModelsInLayerAmount(nativePointer, i);
            for (int j = 0; j < modelsAmount; ++j)
            {
                GameObject model = new GameObject();
                model.name = GetNameFromModel(i, j);
                model.transform.parent = layer.transform;

                GenerateLineRenderer(model, GetPositions3D(i, j), looped);
            }
        }
    }

    void GenerateLineRenderer(GameObject model, Vector3[] positions, bool looped)
    {
        LineRenderer line = model.AddComponent<LineRenderer>();
        line.positionCount = positions.Length;
        line.SetPositions(positions);
        line.startWidth = 0.25f;
        line.endWidth = 0.25f;
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Color"));
        whiteDiffuseMat.SetColor("_Color", Color.green);
        line.material = whiteDiffuseMat;
        line.loop = looped;
    }

    Vector3[] GetPositions3D(int layer, int model)
    {
        Vector2[] positions2D = new Vector2[getVectorsAmount(nativePointer, layer, model)];
        fillVectors(nativePointer, layer, model, positions2D);
        Vector3[] positions3D = new Vector3[positions2D.Length];
        ConvertVector(positions3D, positions2D);
        return positions3D;
    }

    string GetNameFromLayer(int layer)
    {
        var ptr = getLayerName(nativePointer, layer);
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

    public void Clean2D()
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
