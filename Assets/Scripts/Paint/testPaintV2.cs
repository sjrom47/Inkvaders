using UnityEngine;

public class PaintedSurface : MonoBehaviour
{
    private Texture2D floorTexture;
     // Size of the texture (1024x1024 in this case)
    public Renderer SurfaceRenderer { get; set; }


    void Start()
    {
        SurfaceRenderer = GetComponent<Renderer>();

        // Create a new Texture2D at runtime
        floorTexture = new Texture2D(Constants.TEXTURE_SIZE, Constants.TEXTURE_SIZE, TextureFormat.RGBA32, false);

        // Initialize the texture with a default color (e.g., white)
        Color[] initialColors = new Color[Constants.TEXTURE_SIZE * Constants.TEXTURE_SIZE];
        for (int i = 0; i < initialColors.Length; i++)
        {
            initialColors[i] = Color.black; // Set the whole texture to white initially
        }

        floorTexture.SetPixels(initialColors);
        floorTexture.Apply();

        // Set the texture to the material's shader
        Material material = SurfaceRenderer.material;
        material.SetTexture("_Premade_mask", floorTexture); // Assuming you have "_Premade_mask" in your shader
    }

    public Texture2D GetFloorTexture()
    {
        return floorTexture;
    }

    void Update()
    {
        
    }
}
