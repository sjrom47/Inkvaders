using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    ShootCommand shotCommand;
    // Start is called before the first frame update
    void Start()
    {
        shotCommand = new ShootCommand(GetComponentInChildren<ParticleSystem>());
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
        shotCommand.Execute();
    }

    public void StopShooting()
    {
        shotCommand.Undo();
    }
}
