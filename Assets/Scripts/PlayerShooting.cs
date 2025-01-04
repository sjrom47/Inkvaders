using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    //MovementInput input;
    // Start is called before the first frame update
    ShootCommand shootCommand;
    [SerializeField] ParticleSystem shootingParticles;
    [SerializeField] Transform parentController;

    void Start()
    {
        //input = GetComponent<MovementInput>();
        shootCommand = new ShootCommand(shootingParticles);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 angle = parentController.localEulerAngles;
        //input.blockRotationPlayer = Input.GetMouseButton(0);
        bool pressing = Input.GetMouseButton(0);

        //if (Input.GetMouseButton(0))
        //{
            
        //    input.RotateToCamera(transform);
        //}

        if (Input.GetMouseButtonDown(0))
            shootCommand.Execute();
        else if (Input.GetMouseButtonUp(0))
            shootCommand.Undo();

        //parentController.localEulerAngles
        //    = new Vector3(Mathf.LerpAngle(parentController.localEulerAngles.x, pressing ? RemapCamera(freeLookCamera.m_YAxis.Value, 0, 1, -25, 25) : 0, .3f), angle.y, angle.z);


    }

    //float RemapCamera(float value, float from1, float to1, float from2, float to2)
    //{
    //    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    //}
}
