using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SimplexNoise noise = new SimplexNoise();
        GetComponent<Renderer>().material.mainTexture = noise.CreateHeightmap(1024);

        Vector3[] c = new Vector3[3];
        for(int i = 0; i < c.Length; i++)
        {
            c[i].x = 2;
            c[i].y = 3;
            c[i].z = 4;
        }

        Exporter exp = new Exporter();
        exp.SetVertex(c);
        Debug.Log("jejej: " + exp.GetSize());
    }
}
