using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : Command
{
    ParticleSystem shotParticles;

    public ShootCommand(ParticleSystem shotParticles)
    {
        this.shotParticles = shotParticles;
    }

    public override void Execute()
    {
        shotParticles.Play();
    }

    public override void Undo()
    {
        shotParticles.Stop();
    }
}
