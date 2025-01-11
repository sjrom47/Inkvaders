using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{   // TODO: change color to paint so collision shares the paint with other parts of the code
    [SerializeField] Color paintColor;

    [SerializeField] int minRadius = 5;
    [SerializeField] float maxRadius = 10;
    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;
    public Color PaintColor
    {
        get { return paintColor; }
        set
        {
            paintColor = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        PaintedSurface paintedSurface = other.GetComponent<PaintedSurface>();
        if (paintedSurface != null)
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 normalVector = collisionEvents[i].normal;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.Instance().PaintSurface(paintedSurface, pos, normalVector, radius, paintColor);
            }
        }
        Player player = other.GetComponent<Player>();
        if (player != null && player.PlayerColor != PaintColor)
        {
            player.TakeDamage(1f);
        }
    }
}