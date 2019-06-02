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
    private static extern bool export_obj(NativeExporter* ptr, string path);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_vertex(NativeExporter* ptr, Vector3 [] v, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_normals(NativeExporter* ptr, Vector3[] n, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_texcoord(NativeExporter* ptr, Vector2[] tc, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_path(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_log(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_mesh_transform(NativeExporter* ptr, Vector3 position, Vector3 rotation, Vector3 scale);
    
    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int get_size(NativeExporter* ptr);

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


    public bool Export(string path)
    {
        //-----------------------------------------------------------------------
        List<MeshFilter> meshesList = new List<MeshFilter>();

        foreach (GameObject gameobject in Selection.gameObjects)
        {
            if (gameobject.GetComponent<MeshFilter>() != null)
                meshesList.Add(gameobject.GetComponent<MeshFilter>());
        }

        if (meshesList.Count == 0)
        {
            Debug.Log("Ninguna malla seleccionada");
            return false;
        }

        MeshFilter[] meshes = meshesList.ToArray();

        //------------------------------------------------------------------------

        if (Application.isPlaying)
        {
            foreach (MeshFilter filter in meshes)
            {
                MeshRenderer renderer = filter.gameObject.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.isPartOfStaticBatch)
                {
                    Debug.Log("Error malla con static batch");
                    return false;
                }
            }
        }
        //------------------------------------------------------------------------

        for(int i = 0; i < meshes.Length; ++i)
        {
            string name = meshes[i].gameObject.name;
            MeshRenderer renderer = meshes[i].gameObject.GetComponent<MeshRenderer>();


            set_vertex(nativeExporter, meshes[i].sharedMesh.vertices, meshes[i].sharedMesh.vertices.Length);
            set_normals(nativeExporter, meshes[i].sharedMesh.normals, meshes[i].sharedMesh.normals.Length);
            set_texcoord(nativeExporter, meshes[i].sharedMesh.uv, meshes[i].sharedMesh.uv.Length);

            

        }

        
        
        return export_obj(nativeExporter, path);
    }

    public string GetPath()
    {
        var ptr = get_path(nativeExporter);
        string back = Marshal.PtrToStringAnsi(ptr);
        return back;
    }

    public string GetLog()
    {
        var ptr = get_log(nativeExporter);
        string back = Marshal.PtrToStringAnsi(ptr);
        return back;
    }

    public int GetSize()
    {
        return get_size(nativeExporter);
    }

    public void SetVertex(Vector3[] vertex)
    {
        set_vertex(nativeExporter, vertex, vertex.Length);
    }

    public void SetNormals(Vector3 [] normals)
    {
        set_normals(nativeExporter,normals, normals.Length);
    }

    public void SetTexcoord(Vector2 [] texcoord)
    {
        set_texcoord(nativeExporter,texcoord, texcoord.Length);
    }

    public void SetMeshTransform(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        set_mesh_transform(nativeExporter, position, rotation, scale);
    }

    #endregion
}
