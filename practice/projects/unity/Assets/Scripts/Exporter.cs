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
    private static extern bool export_obj(NativeExporter* ptr, string path, string name);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_path(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_log(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_mesh_transform(NativeExporter* ptr, int index, Vector3 position, Vector3 rotation, Vector3 scale);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_mesh_by_index(NativeExporter* ptr, int index, Vector3[] vertex, Vector3[] normals, Vector2[] uvs, int size_v, int size_n, int size_uv);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void set_meshes_count(NativeExporter* ptr, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_mesh_submeshes_count(NativeExporter* ptr, int index, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_submesh_triangles(NativeExporter* ptr, int index, int submesh, int[] triangles, int size);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int get_meshes_count(NativeExporter* ptr);

    #endregion

    NativeExporter* nativeExporter;

    public bool allScene = false;
    public string name = "scene";

    #region API friendly
    public Exporter()
    {
        nativeExporter = create();
    }

    ~Exporter()
    {
        destroy(nativeExporter);
    }


    public bool Export(string path, string fileName)
    {
        //-----------------------------------------------------------------------
        List<MeshFilter> meshesList = new List<MeshFilter>();

        if(allScene)
        {
            foreach(MeshFilter m in UnityEngine.Object.FindObjectsOfType(typeof(MeshFilter)) as MeshFilter[])
            {
                meshesList.Add(m);
            }
        }
        else
        {
            foreach (GameObject gameobject in Selection.gameObjects)
            {
                if (gameobject.GetComponent<MeshFilter>() != null)
                    meshesList.Add(gameobject.GetComponent<MeshFilter>());
            }
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
        Debug.Log("0 " + meshes.Length);
        set_meshes_count(nativeExporter, meshes.Length);

        for (int i = 0; i < meshes.Length; ++i)
        {
            string name = meshes[i].gameObject.name;
            MeshRenderer renderer = meshes[i].gameObject.GetComponent<MeshRenderer>();
            
            if (!set_mesh_transform(nativeExporter, i, meshes[i].transform.position, meshes[i].transform.rotation.eulerAngles, meshes[i].transform.lossyScale))
                return false;
            Debug.Log("1");

            if (!set_mesh_by_index(nativeExporter, i, meshes[i].sharedMesh.vertices, meshes[i].sharedMesh.normals, meshes[i].sharedMesh.uv, meshes[i].sharedMesh.vertices.Length, meshes[i].sharedMesh.normals.Length, meshes[i].sharedMesh.uv.Length))
                return false;
            Debug.Log("2");

            if (!set_mesh_submeshes_count(nativeExporter, i, meshes[i].sharedMesh.subMeshCount))
                return false;
            Debug.Log("3");

            for (int j = 0; j < meshes[i].sharedMesh.subMeshCount; ++j)
            {
                if (!set_submesh_triangles(nativeExporter, i, j, meshes[i].sharedMesh.GetTriangles(j), meshes[i].sharedMesh.GetTriangles(j).Length))
                    return false;
                Debug.Log("4");

            }
        }

        Debug.Log("Todo ok");
        return export_obj(nativeExporter, path, fileName);
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
