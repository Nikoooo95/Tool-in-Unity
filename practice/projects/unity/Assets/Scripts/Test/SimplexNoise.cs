using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// WRAPPER
public unsafe class SimplexNoise
{
    #region Native
    struct NativeSimplexNoise { }

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern NativeSimplexNoise* createSimplexNoise();

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void destroySimplexNoise(NativeSimplexNoise* ptr);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern void initializeSimplexNoise(NativeSimplexNoise* ptr);

    [DllImport("Tool", CallingConvention = CallingConvention.Cdecl)]
    private static extern double simplex(NativeSimplexNoise* ptr, double x, double y);
    #endregion

    NativeSimplexNoise* nativePointer;

    #region API friendly
    public SimplexNoise()
    {
        nativePointer = createSimplexNoise();
        initializeSimplexNoise(nativePointer);
    }

    ~SimplexNoise()
    {
        destroySimplexNoise(nativePointer);
    }

    public double GetNoise(double x, double y)
    {
        return simplex(nativePointer, x, y);
    }

    public Texture2D CreateHeightmap(int size)
    {
        Texture2D texture = new Texture2D(size, size);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noise = (float)GetNoise((double)x * 0.01, (double)y * 0.01); // -1, 1
                noise = noise * 0.5f + 0.5f; // 0, 1
                // ALU
                texture.SetPixel(x, y, Color.white * noise);
            }
        }
        texture.Apply();
        return texture;
    }
    #endregion
}
