using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] ParticleSystem deathEffect;
    private int maxHealth;
    private Slider healthSlider;


    public void Initialize(int health, Slider slider)
    {
        maxHealth = health;
        currentHealth = maxHealth;
        healthSlider = slider;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(1);
        }
    }

    void OnDeath()
    {
        deathEffect.transform.parent = null;
        deathEffect.Play();
        Destroy(deathEffect.gameObject, deathEffect.main.duration);     
        Destroy(gameObject);
    }
}
