using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    /// <summary>
    /// Target is component of Current Object
    /// </summary>
    private Target _target;

    public int damage = 1;
    
    private void Awake()
    {
        _target = GetComponent<Target>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.CompareTag("Player")) 
            return;
        
        var player = other.collider.GetComponent<Player>();
        player.TakeDamage(damage);

        
        _target.Annihilated();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;
        
        var player = other.GetComponent<Player>();
        player.TakeDamage(damage);

        _target.Annihilated();
    }

    
}
