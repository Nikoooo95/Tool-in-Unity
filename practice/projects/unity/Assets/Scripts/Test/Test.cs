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
        Vector3 c = new Vector3(2f, 4f, 6f);
        ex.SetData(c);
        Debug.Log("Numero de mierda: " + ex.GetData());

        Debug.Log("otro: " + ex.GetPath());
    }
}
