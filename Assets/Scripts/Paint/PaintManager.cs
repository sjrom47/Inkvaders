using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.Rendering;


// TODO: see if this should be a monobehaviour Singleton or a regular singleton
public class PaintManager : MonoBehaviourSingleton<PaintManager>
{
    ColorCounter colorCounter;
    List<PaintedSurface> allPaintedSurfaces;
    public CollisionHandler collisionHandler;
    void Awake()
    {
        colorCounter = new ColorCounter();
        allPaintedSurfaces = new List<PaintedSurface>();
        // TODO: see if this should be a singleton a static class or something else
        collisionHandler = CollisionHandler.Instance();
        
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

    public Dictionary<Color,int> OnGameEnd()
    {
        // TODO: create the event to calculate the amount of paint of each team and notify the GameManager
        Dictionary<Color, int> colorCounts = GetAllColorCounts();
        return colorCounts;
    }

    

    public void PaintSurface(PaintedSurface surface, Vector3 pos, Vector3 normalVector,float radius, Color paintColor)
    {
         // TODO: clean this function up
        if (!allPaintedSurfaces.Contains(surface))
        {
            allPaintedSurfaces.Add(surface);
        }
        Vector2 collisionPoint = collisionHandler.FindTextureCollisionPoint(pos, -normalVector);
        if (collisionPoint != null)
        {
            Vector2 pixelCoords = collisionHandler.GetPixelCoords(collisionPoint);
            Texture2D floorTexture = surface.GetFloorTexture();
            int width = floorTexture.width;
            int height = floorTexture.height;
            Color[] colors = floorTexture.GetPixels();
            int pixelRadius = (int)Math.Round(radius,0);
            int radiusSquared = pixelRadius*pixelRadius;
            int minX = Mathf.Min((int)pixelCoords.x - pixelRadius);
            int maxX = Mathf.Max((int)pixelCoords.x + pixelRadius);
            int minY = Mathf.Min((int)pixelCoords.y - pixelRadius);
            int maxY = Mathf.Max((int)pixelCoords.y + pixelRadius);
            int centerX = (int)pixelCoords.x;
            int centerY = (int)pixelCoords.y;
            for (int y = minY; y <= maxY; y++)
            {
                float dy = y - centerY;
                float dySquared = dy * dy;

                for (int x = minX; x <= maxX; x++)
                {
                    float dx = x - centerX;
                    // Check if point is within circle using distance formula
                    if (dx * dx + dySquared <= radiusSquared)
                    {
                        colors[y * width + x] = paintColor;
                    }
                }
            }

            // Apply all pixels at once
            floorTexture.SetPixels(colors);
            //Color[] colorGrid = new Color[2*pixelRadius*4*pixelRadius];
            //for (int i = 0; i < colorGrid.Length; i++)
            //{
            //    colorGrid[i] = paintColor;
            //}
            //Texture2D floorTexture = surface.GetFloorTexture();
            //floorTexture.SetPixels((int)pixelCoords.x-pixelRadius, (int)pixelCoords.y -  pixelRadius, 2*pixelRadius, 2*pixelRadius, colorGrid);
            floorTexture.Apply(); // Apply changes to the texture

            // Update the material with the new texture (if needed)
            //Material material = surface.SurfaceRenderer.material;
            //material.SetTexture("_Premade_mask", floorTexture);

        }
    }

    public Color GetColorOfFloor(Vector3 position)
    {
        RaycastResult collision = collisionHandler.FindCollisionPoint(position, position.y >= 0 ? Vector3.down : Vector3.up);

        if (collisionHandler.CollisionIsTexture(collision))
        {
            
            PaintedSurface surface = collision.Hit.collider.GetComponent<PaintedSurface>();
            if (surface != null)
            {
                Vector2 collisionPoint = collision.Hit.textureCoord;
                
                Vector2 pixelCoords = collisionHandler.GetPixelCoords(collisionPoint);
                Texture2D floorTexture = surface.GetFloorTexture();
                Color pixelColor = floorTexture.GetPixel((int)pixelCoords.x, (int)pixelCoords.y);
                //Color[] carray = new Color[400];
                //for (int i = 0; i < carray.Length; i++)
                //{
                //    carray[i] = Color.red;
                //}
                //floorTexture.SetPixels((int)pixelCoords.x-10, (int)pixelCoords.y-10,20,20, carray);
                //floorTexture.Apply();
                return pixelColor;
            }
        }

        return Color.black;
    }
    
}

