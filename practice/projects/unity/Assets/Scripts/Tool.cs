using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text;

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

    public void Load2D()
    {
        int layersAmount = getLayersAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            GameObject layer = new GameObject();
            layer.name = getNameFromLayer(i);
            int modelsAmount = getModelsInLayerAmount(nativePointer, i);
            for (int j = 0; j < modelsAmount; ++j)
            {
                GameObject model = new GameObject();
                model.name = getNameFromModel(i, j);
                model.transform.parent = layer.transform;

                generateLineRenderer(model, getPositions3D(i, j));
            }
        }
    }

    void generateLineRenderer(GameObject model, Vector3[] positions)
    {
        LineRenderer line = model.AddComponent<LineRenderer>();
        line.positionCount = positions.Length;
        line.SetPositions(positions);
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

    #endregion
}
