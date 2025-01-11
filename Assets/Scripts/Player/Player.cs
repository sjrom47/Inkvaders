using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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
    Image damageOverlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;

    // Health Bar
    private float chipSpeed = 10f;
    private float lerpTimer;
    [SerializeField] Image frontHealthBar;
    [SerializeField] Image backHealthBar;
    [SerializeField] TextMeshProUGUI healthText;

    public bool IsSquid = false;
    public bool isReloading = false;
    public event Action StartReloading;
    public event Action StopReloading;
    public bool isTakingDamage = false;

    private PlayerStateMachine stateMachine;
    GameObject[] childrenComponents;
    IEnumerator DamageCoroutine;
    IEnumerator HealthCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        fadeSpeed = 1;
        PaintManager = PaintManager.Instance();
        health = maxHealth;
        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.Initialize();
        damageOverlay = GameObject.FindWithTag("damage_overlay").GetComponent<Image>();
        frontHealthBar = GameObject.FindWithTag("back_healthbar").GetComponent<Image>();
        backHealthBar = GameObject.FindWithTag("front_healthbar").GetComponent<Image>();
        healthText = GameObject.FindWithTag("health_text").GetComponent<TextMeshProUGUI>();
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 0);
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
            UpdateHealthUI();
            healthText.text = health + "/" + maxHealth;
        }
    }

    public void UpdateHealthUI()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;
        if (fillBack > healthFraction) // we have recieved damage
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }
        if (fillFront < healthFraction) // we have been healed
        {
            backHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, healthFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (CompareTag("Player"))
        {
            durationTimer = 0;
            lerpTimer = 0f;
            damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 1);
        }
        if (health <= 0 && !IsDead)
        {
            PlayerDeath();
        }
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        lerpTimer = 0f;
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
        HealthCoroutine = RestoreConstantHealth();
        StartCoroutine(HealthCoroutine);
    }

    public void StopRestoreConstantHealth()
    {
        if (HealthCoroutine != null)
        {
            StopCoroutine(HealthCoroutine);
        }
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
                    //Debug.Log(child.GetType().Name);
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
        health = maxHealth;
        stateMachine.ChangeState(new PlayerNothingState());
        gameObject.GetComponent<WeaponHolder>().GetCurrentWeapon().FullReload();
        IsDead = false;
        Debug.Log("Finished death cor");
    }
}
