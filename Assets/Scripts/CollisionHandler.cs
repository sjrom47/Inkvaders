using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : Singleton<CollisionHandler>
{

    // TODO: see how we could expand to also work for walls

    public RaycastResult FindCollisionPoint(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))//, Constants.MAX_VERTICAL_DISTANCE_COLLISION))
        {
            

            return new RaycastResult(true, hit);//, objectType);
        }

        // If no collision is detected
        return new RaycastResult(false, new RaycastHit());//, null);
    }



//public Vector2 FindCollisionPoint(Vector3 collisionPoint, Vector3 surfaceNormal)
//{
//    // TODO: check if we should return the surface

//    // Cast a ray from the collision point in the direction of the surface normal
//    Ray ray = new Ray(collisionPoint, -surfaceNormal);  // Assuming we want to cast the ray downwards (into the ground)

//    RaycastHit hit;
//    if (Physics.Raycast(ray, out hit, Constants.MAX_VERTICAL_DISTANCE_COLLISION))
//    {
//        // Get the texture coordinates at the hit point on the mesh
//        Vector2 textureCoords = hit.textureCoord;

//        // Output the texture coordinates

//        return textureCoords;
//        // Optionally, you can use these texture coordinates to interact with the texture
//    }
//    return Vector2.zero;
//}

    public bool CollisionIsTexture(RaycastResult collision)
    {
        return (collision.IsHit && collision.Hit.collider is MeshCollider && collision.Hit.collider.GetComponent<Renderer>() != null);
    }
    public Vector2 FindTextureCollisionPoint(Vector3 collisionPoint, Vector3 surfaceNormal)
    {
        RaycastResult collision = FindCollisionPoint(collisionPoint, surfaceNormal);
        if (CollisionIsTexture(collision))
        {
            // Get the texture coordinates at the hit point on the mesh
            Vector2 textureCoords = collision.Hit.textureCoord;

            // Output the texture coordinates

            return textureCoords;
        }
        return Vector2.zero;
    }
    public Vector2 GetPixelCoords(Vector2 collisionPoint)
    {
        // TODO: check if float is required
        float pointX = (float)Math.Round(Constants.HORIZONTAL_TEXTURE_SIZE * collisionPoint.x, 0);
        float pointY = (float)Math.Round(Constants.VERTICAL_TEXTURE_SIZE * collisionPoint.y, 0);
        return new Vector2(pointX, pointY);
    }

    
}
public struct RaycastResult
{
    public bool IsHit;
    public RaycastHit Hit;
    //public System.Type ObjectType;

    public RaycastResult(bool isHit, RaycastHit hit)//, System.Type objectType)
    {
        IsHit = isHit;
        Hit = hit;
        //ObjectType = objectType;
    }
}