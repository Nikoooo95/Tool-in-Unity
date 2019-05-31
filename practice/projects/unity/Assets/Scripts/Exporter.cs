using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public unsafe class Exporter
{
    #region Native
    struct NativeExporter { }

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern NativeExporter* create();

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void destroy(NativeExporter* ptr);

    [DllImport("ExportTool", CallingConvention = CallingConvention.Cdecl)]
    private static extern int get(NativeExporter* ptr);
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
    
    public int GetData()
    {
        return get(nativeExporter);
    }
    #endregion
}
