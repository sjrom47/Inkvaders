using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PaintManager : Singleton<PaintManager>
{
    ColorCounter colorCounter;
    List<PaintedSurface> allPaintedSurfaces = new List<PaintedSurface>();
    // Start is called before the first frame update
    void Start()
    {
        colorCounter= new ColorCounter();
        StartCoroutine("PaintPercentageCorroutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PaintPercentageCorroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            var colorCounts = GetAllColorCounts();
            foreach (var color in colorCounts)
            {
                Debug.Log(color);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    private Dictionary<Color,int> GetAllColorCounts()
    {
        Dictionary<Color,int> cumulativeResults = new Dictionary<Color,int>();
        foreach (PaintedSurface surface in allPaintedSurfaces)
        {
            var surfaceResult = colorCounter.CountColorsInTexture(surface.GetFloorTexture());
            foreach (var color in surfaceResult.Keys)
            {
                if (cumulativeResults.ContainsKey(color))
                {
                    cumulativeResults[color] += surfaceResult[color];
                }
                else
                {
                    cumulativeResults[color]= surfaceResult[color];
                }
            }
        }
        return cumulativeResults;

    }

    private void OnGameEnd()
    {
        // TODO: create the event to calculate the amount of paint of each team and notify the GameManager
        Dictionary<Color, int> colorCounts = GetAllColorCounts();
    }

    public void PaintSurface(PaintedSurface surface, Vector3 pos, Vector3 normalVector,float radius, Color paintColor)
    {
        if (!allPaintedSurfaces.Contains(surface))
        {
            allPaintedSurfaces.Add(surface);
        }
        Vector2 collisionPoint = FindCollisionPoint(pos,normalVector);
        if (collisionPoint != null)
        {
            Vector2 pixelCoords = new Vector2((float)Math.Round(Constants.TEXTURE_SIZE*collisionPoint.x,0),(float)Math.Round(Constants.TEXTURE_SIZE*collisionPoint.y));
            int pixelRadius = (int)Math.Round(radius,0);
            Color[] colorGrid = new Color[2*pixelRadius*4*pixelRadius];
            for (int i = 0; i < colorGrid.Length; i++)
            {
                colorGrid[i] = paintColor;
            }
            Texture2D floorTexture = surface.GetFloorTexture();
            floorTexture.SetPixels((int)pixelCoords.x-pixelRadius, (int)pixelCoords.y -  pixelRadius, 2*pixelRadius, 2*pixelRadius, colorGrid);
            floorTexture.Apply(); // Apply changes to the texture

            // Update the material with the new texture (if needed)
            Material material = surface.SurfaceRenderer.material;
            material.SetTexture("_Premade_mask", floorTexture);

        }
    }
    Vector2 FindCollisionPoint(Vector3 collisionPoint, Vector3 surfaceNormal)
    {
        //// Get the point of collision from the particle or object
        //Vector3 collisionPoint = collision.contacts[0].point;

        //// Get the surface normal at the collision point
        //Vector3 surfaceNormal = collision.contacts[0].normal;

        // Cast a ray from the collision point in the direction of the surface normal
        Ray ray = new Ray(collisionPoint, -surfaceNormal);  // Assuming we want to cast the ray downwards (into the ground)

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Get the texture coordinates at the hit point on the mesh
            Vector2 textureCoords = hit.textureCoord;

            // Output the texture coordinates
            
            return textureCoords;
            // Optionally, you can use these texture coordinates to interact with the texture
        }
        return Vector2.zero;
    }
}

