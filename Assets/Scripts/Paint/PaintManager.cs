using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.Rendering;


public class PaintManager : MonoBehaviourSingleton<PaintManager>
{
    ColorCounter colorCounter;
    List<PaintedSurface> allPaintedSurfaces;
    public CollisionHandler collisionHandler;
    void Awake()
    {
        colorCounter = new ColorCounter();
        allPaintedSurfaces = new List<PaintedSurface>();
        
        collisionHandler = CollisionHandler.Instance();
        
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
        Dictionary<Color, int> colorCounts = GetAllColorCounts();
        return colorCounts;
    }

    public void PaintSurface(PaintedSurface surface, Vector3 pos, Vector3 normalVector,float radius, Color paintColor)
    {
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
            floorTexture.Apply(); // Apply changes to the texture

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
                return pixelColor;
            }
        }

        return Color.black;
    }
    
}

