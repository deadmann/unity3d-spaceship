using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour
{
    private Transform _gun;
    private float _lastShootTime;

    private HealthBar _healthBar;
    
    
    public GameObject pfBullet;
    public GameObject muzzlePrefab;
    public float shootingCooldown = .25f;
    public float bulletSpeed = 30f;

    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        _gun = transform.Find("Gun");
        _lastShootTime = 0;
    }

    private void Start()
    {
        currentHealth = maxHealth;

        _healthBar = FindObjectOfType<HealthBar>();
        _healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (!Input.GetButton("Jump"))
            return;
        
        if(_lastShootTime + shootingCooldown > Time.time)
            return;
        
        _lastShootTime = Time.time;
        ShootABullet();
    }

    private void ShootABullet()
    {
        var muzzleVfx = Instantiate(muzzlePrefab, _gun.transform.position, Quaternion.identity);
        var psMuzzle = muzzleVfx.GetComponent<ParticleSystem>();
        if (psMuzzle != null)
        {
            Destroy(muzzleVfx, psMuzzle.main.duration);
        }
        else
        {
            var psChild = muzzleVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(muzzleVfx, psChild.main.duration);
        }
            
        var bullet = Instantiate(pfBullet, _gun.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Setup(Vector2.up, bulletSpeed);
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
