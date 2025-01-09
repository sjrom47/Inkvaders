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

    public void SetColor(Color color)
    {
        //ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        //if (particles == null)
        //{
        //    Debug.LogError("particles Dont Exist");
        //}
        //Debug.Log(particles);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        //if (renderer.material.HasProperty("_Color"))
        //{
        //    renderer.material.SetColor("_Color", color);
        //}
        //renderer.material = new Material(renderer.material);
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }

        //ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        //foreach (ParticleSystem particleSystem in particleSystems)
        //{
        //    var trails = particleSystem.trails;
        //    ParticleSystemRenderer particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        //    if (trails.enabled)
        //    {
        //        Debug.Log(trails.enabled);
        //        particleSystemRenderer.material.SetColor("_Color", color);
        //    }

        //}
        //Renderer[] renderers = GetComponentsInChildren<Renderer>();
        //foreach (Renderer renderer in renderers)
        //{
        //    // Create a material instance for each renderer
        //    Material materialInstance = new Material(renderer.sharedMaterial);
        //    renderer.material = materialInstance;
        //    materialInstance.color = color;
        //}

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
        //particles.gameObject.GetComponent<Renderer>().material.color = color;
        ParticleCollision particleCollision = GetComponentInChildren<ParticleCollision>();
        particleCollision.PaintColor = color;
    }
    public void Shoot()
    {
        Debug.Log(shotCommand);
        if (amunition > 0)
        {
            shootingCoroutine = RemoveAmunition();
            shotCommand.Execute();
            StartCoroutine(shootingCoroutine);
        }
    }

    public void StopShooting()
    {
        shotCommand.Undo();
        StopCoroutine(shootingCoroutine);
    }

    public void Reload()
    {
        reloadingCoroutine = ReloadAmunition();
        StartCoroutine(reloadingCoroutine);
    }

    public void StopReloading()
    {
        StopCoroutine(reloadingCoroutine);
    }

    public IEnumerator ReloadAmunition()
    {
        while (true)
        {
            Debug.Log(amunition);
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
            Debug.Log(amunition);
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
