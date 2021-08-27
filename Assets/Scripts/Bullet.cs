using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameController _gameController;
    private Vector3 _shootDir;

    private float _moveSpeed;

    
    public GameObject hitPrefab;

    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void Setup(Vector3 shootDirection, float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _shootDir = shootDirection;
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += _shootDir * Time.deltaTime * _moveSpeed;

        var boarders = GameController.GetBoarders();
        if (transform.position.y > boarders.top)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hitPrefab != null)
        {
            var position = other.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, position.normalized);
            
            var hitVfx = Instantiate(hitPrefab, position, rotation);
            var psHis = hitVfx.GetComponent<ParticleSystem>();
            if (psHis != null)
            {
                Destroy(hitVfx, psHis.main.duration);
            }
            else
            {
                var psChild = hitVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVfx, psChild.main.duration);
            }
        }
        
        var target = other.GetComponentInParent<Target>();
        if (target != null)
        {
            target.Damage(3);
            Destroy(gameObject);
        }
    }
}
