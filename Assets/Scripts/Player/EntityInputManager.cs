using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInputManager : MonoBehaviour
{
    EntityController entityController;
    float horizontalStraightMovementInput;
    float horizontalSideMovementInput;
    // Start is called before the first frame update
    void Start()
    {
        entityController = GetComponent<EntityController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    playerController.TransformIntoSquid();
        //}
        //else if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    playerController.EndSquidTransformation();
        //}
        horizontalStraightMovementInput = Input.GetAxis("Vertical");
        horizontalSideMovementInput = Input.GetAxis("Horizontal");
        entityController.ApplyMovement(horizontalStraightMovementInput, horizontalSideMovementInput);
    }
}
