using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBost : MonoBehaviour
{
    [SerializeField]
    public float multiplier = 1.5f;

    private CharacterMovement characterMovement;

    private void Start()
    {
        // Obtener el componente CharacterMovement del jugador
        characterMovement = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerBody")
        {
            characterMovement.moveSpeed *= multiplier; // Acceder a moveSpeed a través de la referencia
            Destroy(gameObject);
        }
    }
}
