using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Color PlayerColor {  get; set; }

    // Health
    private float health;
    public float maxHealth;

    // Damage Overlay
    public Image damageOverlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerColor = Color.black;
    }

    // Update is called once per frame
    void Update()
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        durationTimer = 0;
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 1);
        Debug.Log(health);
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        Debug.Log(health);
    }
}
