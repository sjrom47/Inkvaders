using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPaintold : MonoBehaviour
{
    // Start is called before the first frame update
    private Texture2D floorTexture;
    int maskTextureID = Shader.PropertyToID("_Premade_mask");
    //RenderTexture maskRenderTexture;
    int TEXTURE_SIZE = 1024;
    Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Material material = renderer.material;
        //maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        //maskRenderTexture.filterMode = FilterMode.Bilinear;
        //renderer.material.SetTexture(maskTextureID, maskRenderTexture);
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
            floorTexture = (Texture2D)renderer.material.GetTexture(maskTextureID);
            floorTexture.SetPixels(100, 100, 1000, 1000,carray);
        }
    }

    //    private void OnDisable()
    //    {
    //        maskRenderTexture.Release();
    //    }
}
