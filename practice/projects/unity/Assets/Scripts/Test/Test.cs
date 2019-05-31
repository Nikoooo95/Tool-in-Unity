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

        Exporter ex = new Exporter();
        int d = ex.GetData();
        Debug.Log("Numero de mierda: " + d);
    }
}
