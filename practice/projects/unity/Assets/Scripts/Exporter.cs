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

    //[DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    //private static extern void set_vertex(NativeExporter* ptr, Vector3[] v, int size);

    //[DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    //private static extern void set_normals(NativeExporter* ptr, Vector3[] n, int size);

    //[DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    //private static extern void set_texcoord(NativeExporter* ptr, Vector2[] tc, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_path(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_log(NativeExporter* ptr);

    //[DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    //private static extern int get_size(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_mesh_transform(NativeExporter* ptr, int index, Vector3 position, Vector3 rotation, Vector3 scale);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_mesh_by_index(NativeExporter* ptr, int index, Vector3[] vertex, Vector3[] normals, Vector2[] uvs, int size_v, int size_n, int size_uv);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_meshes_count(NativeExporter* ptr, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_mesh_submeshes_count(NativeExporter* ptr, int index, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_submesh_triangles(NativeExporter* ptr, int index, int submesh, int[] triangles, int size);
    

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

        set_meshes_count(nativeExporter, meshes.Length);
        for (int i = 0; i < meshes.Length; ++i)
        {
            string name = meshes[i].gameObject.name;
            MeshRenderer renderer = meshes[i].gameObject.GetComponent<MeshRenderer>();

            if (!set_mesh_transform(nativeExporter, i, meshes[i].transform.position, meshes[i].transform.rotation.eulerAngles, meshes[i].transform.lossyScale))
                return false;
            if(!set_mesh_by_index(nativeExporter, i, meshes[i].sharedMesh.vertices, meshes[i].sharedMesh.normals, meshes[i].sharedMesh.uv, meshes[i].sharedMesh.vertices.Length, meshes[i].sharedMesh.normals.Length, meshes[i].sharedMesh.uv.Length))
                return false;
           
            if(!set_mesh_submeshes_count(nativeExporter, i, meshes[i].sharedMesh.subMeshCount))
                return false;

            for (int j = 0; j < meshes[i].sharedMesh.subMeshCount; ++j)
            {
                if (!set_submesh_triangles(nativeExporter, i, j, meshes[i].sharedMesh.GetTriangles(j), meshes[i].sharedMesh.GetTriangles(j).Length))
                    return false;
            }
        }

        Debug.Log("Todo ok");


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


    #endregion
}
