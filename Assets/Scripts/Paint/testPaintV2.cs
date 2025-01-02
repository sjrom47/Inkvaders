using System;
using UnityEngine;

public class TestPaint : MonoBehaviour
{
    private Texture2D floorTexture;

    void Start()
    {
        // Get the texture from the material on the object's renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Debug.Log(renderer.material);
            Texture2D originalTexture = renderer.material.mainTexture as Texture2D;
            if (originalTexture != null)
            {
                // Create a copy of the original texture so we don't overwrite it
                floorTexture = new Texture2D(originalTexture.width, originalTexture.height, originalTexture.format, false);
                floorTexture.SetPixels(originalTexture.GetPixels());
                floorTexture.Apply();

                // Assign the copy to the material
                renderer.material.mainTexture = floorTexture;
            }
        }
        else
        {
            Debug.LogError("No Renderer found on the GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (floorTexture != null)
            {
                // Define the paint area size
                int paintWidth = 50;
                int paintHeight = 50;
                int x = 100; // X position of the paint area
                int y = 100; // Y position of the paint area

                // Create the paint color array
                Color[] carray = new Color[paintWidth * paintHeight];
                for (int i = 0; i < carray.Length; i++)
                {
                    carray[i] = Color.red;
                }

                // Paint on the texture
                floorTexture.SetPixels(x, y, paintWidth, paintHeight, carray);
                floorTexture.Apply(); // Apply changes to update the GPU
            }
        }
    }
}
