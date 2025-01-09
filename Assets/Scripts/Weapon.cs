using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    ShootCommand shotCommand;
    ParticleSystem weaponParticleSystem;
    float maxAmunitionCapacity = 100;
    float amunition;
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

    }
    public void Shoot()
    {
        Debug.Log(shotCommand);
        if (amunition > 0)
        {
            shotCommand.Execute();
            StartCoroutine(RemoveAmunition());
        }
    }

    public void StopShooting()
    {
        shotCommand.Undo();
        StopCoroutine(RemoveAmunition());
    }

    public void Reload()
    {
        StartCoroutine(ReloadAmunition());
    }

    public void StopReloading()
    {
        StopCoroutine(ReloadAmunition());
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
