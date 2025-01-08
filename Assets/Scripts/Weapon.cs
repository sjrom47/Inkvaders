using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    ShootCommand shotCommand;
    ParticleSystem weaponParticleSystem;
    // Start is called before the first frame update
    void Awake()
    {
        weaponParticleSystem = GetComponentInChildren<ParticleSystem>();
        shotCommand = new ShootCommand(weaponParticleSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color color)
    {

    }
    public void Shoot()
    {
        Debug.Log(shotCommand);
        shotCommand.Execute();
    }

    public void StopShooting()
    {
        shotCommand.Undo();
    }
}
