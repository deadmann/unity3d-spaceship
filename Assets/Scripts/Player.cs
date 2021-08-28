using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour
{
    private HealthBar _healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        _healthBar = FindObjectOfType<HealthBar>();
        _healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        _healthBar.SetHealth(currentHealth);
        
        if (currentHealth > 0) 
            return;
        
        var gameController = FindObjectOfType<GameController>();

        transform.Find("BigBangParticle").GetComponent<ParticleSystem>()
            .Play();
        
        Destroy(gameObject, .5f);
        
        gameController.TakeNewShip();
    }
}
