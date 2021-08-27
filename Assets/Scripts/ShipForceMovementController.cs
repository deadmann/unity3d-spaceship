using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipForceMovementController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public float maxForwardVelocity = 3;
    public float maxBackwardVelocity = 2;
    public float maxSideManeuverVelocity = 2.5f;

    public float topMarginPercent = 70;
    public float bottomMarginPercent = 5;
    public float sideMarginPercent = 5;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var yAxis = Input.GetAxis("Vertical");
        var xAxis = Input.GetAxis("Horizontal");
        
        ThrustForward(yAxis);
        ManeuverToSide(xAxis);
        
        ClampVelocity();
        ClampPosition();
    }

    private void ClampPosition()
    {
        if(Camera.main is null)
            return;
        
        var screenBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        float halfWidth= transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float halfHeight= transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        var topMargin = screenBound.y * 2 / 100 * topMarginPercent;
        var bottomMargin = screenBound.y * 2 / 100 * bottomMarginPercent;
        var sideMargin = screenBound.x * 2 / 100 * sideMarginPercent;
        
        float x = Mathf.Clamp(transform.position.x, -screenBound.x + sideMargin + halfWidth, screenBound.x - sideMargin - halfWidth);
        float y = Mathf.Clamp(transform.position.y, -screenBound.y + bottomMargin + halfHeight, screenBound.y - topMargin - halfHeight);
        
        _rigidbody.position = new Vector2(x, y);
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(_rigidbody.velocity.x, -maxSideManeuverVelocity, maxSideManeuverVelocity);
        float y = Mathf.Clamp(_rigidbody.velocity.y, -maxBackwardVelocity, maxForwardVelocity);

        _rigidbody.velocity = new Vector2(x, y);
    }
    
    private void ThrustForward(float yAxis)
    {
        var force = Vector2.up * yAxis;
        _rigidbody.AddForce(force);
    }

    private void ManeuverToSide(float xAxis)
    {
        var force = Vector2.right * xAxis;
        _rigidbody.AddForce(force);
    }
}
