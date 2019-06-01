using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

public unsafe class Exporter
{
    #region Native
    struct NativeExporter { }

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern NativeExporter* create();

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void destroy(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern Vector3 get(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set(NativeExporter* ptr, Vector3 vector);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool export_obj(NativeExporter* ptr, char[] path);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern string get_path(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_vertex(NativeExporter* ptr, Vector3 texcoord);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_normals(NativeExporter* ptr, Vector3 texcoord);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_texcoord(NativeExporter* ptr, Vector3 texcoord);
    
    #endregion

    NativeExporter* nativeExporter;

    #region API friendly
    public Exporter()
    {
        nativeExporter = create();
    }

    ~Exporter()
    {
        destroy(nativeExporter);
    }
    
    public Vector3 GetData()
    {
        return get(nativeExporter);
    }

    public void SetData(Vector3 vector)
    {
        set(nativeExporter, vector);
    }

    public bool Export(string path)
    {
        //-----------------------------------------------------------------------
        List<MeshFilter> meshesList = new List<MeshFilter>();

        foreach(GameObject gameobject in Selection.gameObjects)
        {
            if (gameobject.GetComponent<MeshFilter>() != null)
                meshesList.Add(gameobject.GetComponent<MeshFilter>());
        }

        if(meshesList.Count == 0)
        {
            Debug.Log("Ninguna malla seleccionada");
            return false;
        }

        MeshFilter[] meshes = meshesList.ToArray();

        //------------------------------------------------------------------------
        
        if(Application.isPlaying)
        {
            foreach(MeshFilter filter in meshes)
            {
                MeshRenderer renderer = filter.gameObject.GetComponent<MeshRenderer>();
                if(renderer != null && renderer.isPartOfStaticBatch)
                {
                    Debug.Log("Error malla con static batch");
                    return false;
                }
            }
        }
        //------------------------------------------------------------------------
        


        return true;
        //return export_obj(nativeExporter, path);
    }

    public string GetPath()
    {
        return get_path(nativeExporter);
    }

    public void SetVertex(Vector3 vertex)
    {
        set_vertex(nativeExporter, vertex);
    }

    public void SetNormals(Vector3 vertex)
    {
        set_normals(nativeExporter, vertex);
    }

    public void SetTexcoord(Vector3 vertex)
    {
        set_texcoord(nativeExporter, vertex);
    }

    #endregion
}
