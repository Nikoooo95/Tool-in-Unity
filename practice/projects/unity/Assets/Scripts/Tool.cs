using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text;

public unsafe class Tool
{
    #region Native
    struct NativeTool { }


    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern NativeTool* createTool();

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool parseFile(NativeTool* tool, string path);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getLayer(NativeTool* tool);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr getVertex();

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void getVector(NativeTool* ptr, Vector2[] prue);

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
    private static extern void fillVectors(NativeTool* ptr, int layer, int model, Vector2[] vectors);

    #endregion
    NativeTool* nativePointer;
    GameObject manager;

    #region API friendly



    public Tool(string path)
    {
        nativePointer = createTool();
        Debug.Log("Parseando...");
        var ptr = parseFile(nativePointer, path);


        Debug.Log(ptr);

    }

    public string getLayerName()
    {
        var ptr = getLayer(nativePointer);
        return Marshal.PtrToStringAnsi(ptr);

    }


    public float getVertex2D()
    {

        //Vector2f* data = (Vector2f*)getVertex();
        // SetVector();
        //return data->x;//(float)System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector2f));
        return 1.0f;

    }

    public void SetVector()
    {
        Vector2[] prueba;
        prueba = new Vector2[2];
        Debug.Log("ANTES -> Prueba[0] X: " + prueba[0].x + " Prueba[0] Y: " + prueba[0].y);
        Debug.Log("ANTES -> Prueba[1] X: " + prueba[1].x + " Prueba[1] Y: " + prueba[1].y);
        getVector(nativePointer, prueba);
        Debug.Log("DESPUES -> Prueba[0] X: " + prueba[0].x + " Prueba[0] Y: " + prueba[0].y);
        Debug.Log("DESPUES -> Prueba[1] X: " + prueba[1].x + " Prueba[1] Y: " + prueba[1].y);
    }
    /* public object MarshalNativeToManaged(System.IntPtr pNativeData)
     {
         Marshal.PtrToStructure(pNativeData, this.prueba);
         int count = 2;//this.marshaledObj.mStruct.count;
         IntPtr pIStruct = this.prueba.;
         InnerStruct[] iStructArray = new InnerStruct[count];
         int dataSize = Marshal.SizeOf(new InnerStruct());
         for (int i = 0; i < count; i++)
         {
             iStructArray[i] = (InnerStruct)Marshal.PtrToStructure(new IntPtr(pIStruct.ToInt32() + dataSize * i), typeof(InnerStruct));
         }

         //*** how do I include iStructArray in return?

         return this.marshaledObj;
     }*/


    public void Load2D()
    {
        int layersAmount = getLayersAmount(nativePointer);
        for (int i = 0; i < layersAmount; ++i)
        {
            GameObject layer = new GameObject();
            getLayerName(nativePointer, i);
            layer.name = getLayer(i);
            int modelsAmount = getModelsInLayerAmount(nativePointer, i);
            for (int j = 0; j < modelsAmount; ++j)
            {
                GameObject model = new GameObject();
                model.name = getModel(i, j);
                model.transform.parent = layer.transform;
                LineRenderer line = model.AddComponent<LineRenderer>();
                Vector2[] positions;
                Debug.Log(getVectorsAmount(nativePointer, i, j));
                positions = new Vector2[getVectorsAmount(nativePointer, i, j)];
               /* fillVectors(nativePointer, i, j, positions);
                Vector3[] pos = new Vector3[positions.Length];
                convertTo(pos, positions);
                line.SetPositions(pos);*/
            }
        }

    }

    string getLayer(int i)
    {
        var ptr = getLayerName(nativePointer, i);
        return Marshal.PtrToStringAnsi(ptr);
    }

    string getModel(int layer, int model)
    {
        var ptr = getModelNameInLayer(nativePointer, layer, model);
        return Marshal.PtrToStringAnsi(ptr);
    }

    void convertTo(Vector3[] vec3, Vector2[] vec2)
    {
        for (int i = 0; i < vec3.Length; ++i)
        {
            vec3[i] = vec2[i];
        }
    }
    #endregion
}
