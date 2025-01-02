using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintWithRenderTexture : MonoBehaviour
{
    public Texture2D sourceTexture;  // The original texture to paint on
    private RenderTexture renderTexture;  // The render texture that we'll paint on
    private Material material;  // Material to apply the texture to
    private int TEXTURE_SIZE = 1024;  // Size of the texture

    void Start()
    {
        material = GetComponent<Renderer>().material;

        // Create a RenderTexture to use as the painting surface
        renderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 24);
        renderTexture.filterMode = FilterMode.Bilinear;
        renderTexture.Create();
        

        // Set the initial texture (e.g., source texture) to the RenderTexture
        Graphics.Blit(sourceTexture, renderTexture);

        // Set the RenderTexture as the material texture for the _Premade_mask property
        material.SetTexture("_Premade_mask", renderTexture);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // When mouse is clicked
        {
            // For illustration, create a simple painting texture (red square for painting)
            Texture2D paintTexture = CreatePaintMask();

            // Blit the paint texture onto the RenderTexture
            Graphics.Blit(paintTexture, renderTexture);
        }
    }

    // This method generates a small red square texture to simulate painting
    Texture2D CreatePaintMask()
    {
        // Create a texture with the same size as the target render texture (1024x1024)
        Texture2D paintMask = new Texture2D(TEXTURE_SIZE, TEXTURE_SIZE);
        Color[] colors = new Color[100 * 100];

        // Fill the texture with a red color
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.red;
        }

        paintMask.SetPixels(100,100,100,100, colors);
        paintMask.Apply();
        return paintMask;
    }

    void OnDestroy()
    {
        // Clean up the RenderTexture when done
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }
}

