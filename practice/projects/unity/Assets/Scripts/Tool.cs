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

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hola();

    #endregion

    NativeTool* nativePointer;

    #region API friendly
    public Tool(string path){

        nativePointer = createTool();
        Debug.Log("Parsing...");
        var ptr = parseFile(nativePointer, path);
        Debug.Log("Parsed.");

    }

    public void Load2D(bool looped)
    {
        int layersAmount = getLayersAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            GameObject layer = new GameObject();
            layer.name = getNameFromLayer(i);
            layer.tag = "Layer";
            int modelsAmount = getModelsInLayerAmount(nativePointer, i);
            for (int j = 0; j < modelsAmount; ++j)
            {
                GameObject model = new GameObject();
                model.name = getNameFromModel(i, j);
                model.transform.parent = layer.transform;

                generateLineRenderer(model, getPositions3D(i, j), looped);
            }
        }
    }

    void generateLineRenderer(GameObject model, Vector3[] positions, bool looped)
    {
        LineRenderer line = model.AddComponent<LineRenderer>();
        line.positionCount = positions.Length;
        line.SetPositions(positions);
        line.startWidth = 0.25f;
        line.endWidth = 0.25f;
        Material material = Resources.Load("Line2D.mat", typeof(Material)) as Material;
        line.sharedMaterial = new Material(Shader.Find("Specular"));
        line.sharedMaterial.color = Color.black;
        line.loop = looped;
    }

    Vector3[] getPositions3D(int layer, int model)
    {
        Vector2[] positions2D = new Vector2[getVectorsAmount(nativePointer, layer, model)];
        fillVectors(nativePointer, layer, model, positions2D);
        Vector3[] positions3D = new Vector3[positions2D.Length];
        convertVector(positions3D, positions2D);
        return positions3D;
    }

    string getNameFromLayer(int layer)
    {
        var ptr = getLayerName(nativePointer, layer);
        return Marshal.PtrToStringAnsi(ptr);
    }

    string getNameFromModel(int layer, int model)
    {
        var ptr = getModelNameInLayer(nativePointer, layer, model);
        return Marshal.PtrToStringAnsi(ptr);
    }

    void convertVector(Vector3[] vec3, Vector2[] vec2)
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
                //Create dialog to ask if User is sure they want to overwrite existing Prefab
                if (EditorUtility.DisplayDialog("Are you sure?",
                    "The Prefab already exists in the path " + savePath + " . Do you want to overwrite it?",
                    "Yes",
                    "No"))
                //If the user presses the yes button, create the Prefab
                {
                    CreatePrefab(savePath, gameObject);
                }
            }
            //If the name doesn't exist, create the new Prefab
            else
            {
                Debug.Log(gameObject.name + " is not a Prefab, will convert");
                CreatePrefab(savePath, gameObject);
            }
        }
    }

    void CreatePrefab(string savePath, GameObject gameObject)
    {
        UnityEngine.Object prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, savePath);
    }

    #endregion
}
