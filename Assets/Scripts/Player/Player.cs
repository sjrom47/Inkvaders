using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Color PlayerColor { get; set; }
    public PaintManager PaintManager { get; set; }
    public Vector3 SpawnPoint { get; set; }
    [SerializeField] float deathTime = 5f;

    // Health
    private float health;
    public float maxHealth;
    public bool IsDead { get; private set; }

    // Damage Overlay
    public Image damageOverlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;

    public bool IsSquid = false;
    public bool isReloading = false;
    public event Action StartReloading;
    public event Action StopReloading;
    public bool isTakingDamage = false;

    private PlayerStateMachine stateMachine;
    GameObject[] childrenComponents;
    IEnumerator DamageCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerColor = Color.black;
        //childrenComponents = gameObject.GetComponentsInChildren<GameObject>();
        PaintManager = PaintManager.Instance();
        health = maxHealth;
        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("Player"))
        {
            if (damageOverlay.color.a > 0)
            {
                durationTimer += Time.deltaTime;
                if (durationTimer > 0)
                {
                    float tempAlpha = damageOverlay.color.a;
                    tempAlpha -= Time.deltaTime * fadeSpeed;
                    damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, tempAlpha);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (CompareTag("Player"))
        {
            durationTimer = 0;
            damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 1);
        }
        if (health <= 0)
        {
            PlayerDeath();
            //StopTakeConstantDamage();
        }
        Debug.Log(health);
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        //Debug.Log(health);
    }

    public void StartTakeConstantDamage()
    {
        DamageCoroutine = TakeConstantDamage();
        StartCoroutine(DamageCoroutine);
    }

    public void StopTakeConstantDamage()
    {
        if (DamageCoroutine != null)
        {
            StopCoroutine(DamageCoroutine);
        }
    }

    public void StartRestoreConstantHealth()
    {
        StartCoroutine(RestoreConstantHealth());
    }

    public void StopRestoreConstantHealth()
    {
        StopCoroutine(RestoreConstantHealth());
    }

    IEnumerator TakeConstantDamage()
    {
        while (true)
        {
            TakeDamage(1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator RestoreConstantHealth()
    {
        while (true)
        {
            RestoreHealth(1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void InvokeStartReloading()
    {
        StartReloading?.Invoke();
    }

    public void InvokeStopReloading()
    {
        StopReloading?.Invoke();
    }

    void PlayerDeath()
    {
        IsDead = true;
        StartCoroutine(DeathCoroutine());
        //stateMachine.ChangeState(new PlayerNothingState());
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "JellyFishGirl":
                    child.gameObject.SetActive(false);
                    break;
                case "CharacterHitbox":
                    child.gameObject.SetActive(true);
                    break;
                case "SquidHitbox":
                    child.gameObject.SetActive(false);
                    break;
                case "Squid_LOD2":
                    child.gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log(child.GetType().Name);
                    break;
            }
            // Deactivate the child GameObject
            
        }
    }

    IEnumerator DeathCoroutine()
    {
        Debug.Log("Started Death time");
        yield return new WaitForSeconds(deathTime);
        gameObject.transform.position = SpawnPoint;
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "JellyFishGirl")
            {
                child.gameObject.SetActive(true);
            }
        }
        //foreach (GameObject gameObject in childrenComponents)
        //{
        //    gameObject.SetActive(true);
        //}
        health = maxHealth;
        stateMachine.ChangeState(new PlayerNothingState());
        gameObject.GetComponent<WeaponHolder>().GetCurrentWeapon().FullReload();
        IsDead = false;
        Debug.Log("Finished death cor");
    }
}
