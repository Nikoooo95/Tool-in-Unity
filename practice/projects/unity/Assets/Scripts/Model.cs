using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text;

public unsafe class Model
{
    #region Native
    struct NativeModel { }

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern NativeModel* createModel();

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern string initializeModel(NativeModel* ptr, IntPtr path);

    #endregion

    NativeModel* nativePointer;

    #region API friendly
    public Model(string path)
    {
        nativePointer = createModel();
        IntPtr newPath = strange(path);
       Debug.Log(initializeModel(nativePointer, newPath));
    }

    public IntPtr strange(object obj)
    {
        var str = obj as string;
        int bytes = Encoding.UTF8.GetByteCount(str);
        byte[] buffer = new byte[bytes + 1];
        Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);
        IntPtr nativeUtf8 = Marshal.AllocHGlobal(buffer.Length);
        Marshal.Copy(buffer, 0, nativeUtf8, buffer.Length);
        return nativeUtf8;

    }
    #endregion
}
