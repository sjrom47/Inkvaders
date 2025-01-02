using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPaint : MonoBehaviour
{
    // Start is called before the first frame update
    private Texture2D floorTexture;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material material = GetComponent<Material>();
        floorTexture = (Texture2D)material.GetTexture("_Premade_Mask");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Color[] carray = new Color[1000* 1000];
            for (int i = 0; i < carray.Length; i++) {
                carray[i] = Color.red;
            }
            floorTexture.SetPixels(100, 100, 1000, 1000,carray);
        }
    }
}
