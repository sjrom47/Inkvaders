using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBost : MonoBehaviour
{
    [SerializeField]
    public float multiplier = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerBody")
        {
            CharacterMovement.speed *= multiplier;
            Destroy(gameObject);
        }
    }
}
