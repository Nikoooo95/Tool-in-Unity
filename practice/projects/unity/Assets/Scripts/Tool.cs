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


    #endregion

    NativeTool* nativePointer;

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
        //return "to bien";
    }

  
    #endregion
}
