using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    ShootCommand shotCommand;
    ParticleSystem weaponParticleSystem;
    float maxAmunitionCapacity = 100;
    float amunition;
    IEnumerator shootingCoroutine;
    IEnumerator reloadingCoroutine;
    // Start is called before the first frame update
    void Awake()
    {
        weaponParticleSystem = GetComponentInChildren<ParticleSystem>();
        shotCommand = new ShootCommand(weaponParticleSystem);
        amunition = maxAmunitionCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Amunition()
    {
        return amunition;
    }

    public void SetColor(Color color)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }

        // For particle systems
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var trails = particleSystem.trails;
            ParticleSystemRenderer particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
            if (trails.enabled)
            {
                // Create new material instance for the trails
                Material trailMaterialInstance = new Material(particleSystemRenderer.trailMaterial);
                particleSystemRenderer.trailMaterial = trailMaterialInstance;
                trailMaterialInstance.SetColor("_Color", color);
            }
        }

        ParticleCollision particleCollision = GetComponentInChildren<ParticleCollision>();
        particleCollision.PaintColor = color;
    }
    public void Shoot()
    {
        if (amunition > 0)
        {
            shootingCoroutine = RemoveAmunition();
            shotCommand.Execute();
            StartCoroutine(shootingCoroutine);
        }
    }
    public void FullReload()
    {
        amunition = maxAmunitionCapacity;
    }

    public void StopShooting()
    {
        shotCommand.Undo();
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }
    }

    public void Reload()
    {
        reloadingCoroutine = ReloadAmunition();
        StartCoroutine(reloadingCoroutine);
    }

    public void StopReloading()
    {
        if (reloadingCoroutine != null)
        {
            StopCoroutine(reloadingCoroutine);
        }
    }

    public IEnumerator ReloadAmunition()
    {
        while (true)
        {
            amunition++;
            if (amunition >= maxAmunitionCapacity)
            {
                StopReloading();
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator RemoveAmunition()
    {
        while (true)
        {
            amunition--;
            if (amunition <= 0)
            {
                StopShooting();
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
