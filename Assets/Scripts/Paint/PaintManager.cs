using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PaintManager : Singleton<PaintManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameEnd()
    {
        // TODO: create the event to calculate the amount of paint of each team and notify the GameManager
    }

    public void PaintSurface(PaintedSurface surface, Vector3 pos, Vector3 normalVector,float radius, Color paintColor)
    {
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

