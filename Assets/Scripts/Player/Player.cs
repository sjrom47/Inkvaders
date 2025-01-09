using System;
using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        PlayerColor = Color.black;
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
        StartCoroutine(TakeConstantDamage());
    }

    public void StopTakeConstantDamage()
    {
        StopCoroutine(TakeConstantDamage());
    }

    public void StartRestoreConstantHealth()
    {
        StartCoroutine(RestoreConstantHealth());
    }

    public void StopRestoreConstantHealth()
    {
        StopCoroutine(RestoreConstantHealth());
    }

    public IEnumerator TakeConstantDamage()
    {
        while (true)
        {
            TakeDamage(1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator RestoreConstantHealth()
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
        gameObject.SetActive(false);
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(deathTime);
        gameObject.transform.position = SpawnPoint;
        gameObject.SetActive(true);
        health = maxHealth;
    }
}
