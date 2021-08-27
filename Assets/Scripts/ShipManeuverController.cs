using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManeuverController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public float maxVelocity = 3;
    public float rotationSpeed = 3;
    
    #region ManeuveringApi
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        
        ThrustForward(yAxis);
        Rotate(transform, xAxis);
        
        // ClampVelocity();
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(_rigidbody.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(_rigidbody.velocity.y, -maxVelocity, maxVelocity);

        _rigidbody.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = (transform.up * amount) * (Time.deltaTime*100);
        
        _rigidbody.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        // t.Rotate(0,0,amount  * -rotationSpeed);
        _rigidbody.MoveRotation(_rigidbody.rotation + (-amount * rotationSpeed) * (Time.deltaTime*100) );
    }
    
    #endregion ManeuveringApi
}
