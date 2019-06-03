using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator instance = null;

    void Start()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        
    }


}
