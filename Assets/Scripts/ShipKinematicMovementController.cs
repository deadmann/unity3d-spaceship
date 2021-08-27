using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ShipKinematicMovementController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public float speed = 10f;
    // public float maxForwardVelocity = 3;
    // public float maxBackwardVelocity = 2;
    // public float maxSideManeuverVelocity = 2.5f;

    public float topMarginPercent = 70;
    public float bottomMarginPercent = 5;
    public float sideMarginPercent = 5;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var yAxis = Input.GetAxis("Vertical");
        var xAxis = Input.GetAxis("Horizontal");

        var currentPos = (Vector2) transform.position;
        ThrustForward(ref currentPos,yAxis);
        ManeuverToSide(ref currentPos,xAxis);
        
        ClampVelocity(ref currentPos);
        ClampPosition(ref currentPos);
        
        _rigidbody.MovePosition(currentPos);
    }

    private void ClampPosition(ref Vector2 currentPos)
    {
        if(Camera.main is null)
            return;
        
        var screenBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        float halfWidth= transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float halfHeight= transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        
        var topMargin = screenBound.y * 2 / 100 * topMarginPercent;
        var bottomMargin = screenBound.y * 2 / 100 * bottomMarginPercent;
        var sideMargin = screenBound.x * 2 / 100 * sideMarginPercent;
        
        
        float x = Mathf.Clamp(currentPos.x, -screenBound.x + sideMargin + halfWidth, screenBound.x - sideMargin - halfWidth);
        float y = Mathf.Clamp(currentPos.y, -screenBound.y + bottomMargin + halfHeight, screenBound.y - topMargin - halfHeight);
        
        currentPos.x = x;
        currentPos.y = y;
    }

    private void ClampVelocity(ref Vector2 currentPos)
    {
        // float x = Mathf.Clamp(_rigidbody.velocity.x, -maxSideManeuverVelocity, maxSideManeuverVelocity);
        // float y = Mathf.Clamp(_rigidbody.velocity.y, -maxBackwardVelocity, maxForwardVelocity);
        //
        // _rigidbody.position = new Vector2(x, y);
    }
    
    private void ThrustForward(ref Vector2 currentPos, float yAxis)
    {
        var movement = Vector2.up * yAxis * speed * Time.fixedDeltaTime;
        var newPosition = (currentPos + movement);
        currentPos = newPosition;
    }

    private void ManeuverToSide(ref Vector2 currentPos, float xAxis)
    {
        var movement = Vector2.right * xAxis * speed * Time.fixedDeltaTime;;
        var newPosition = currentPos + movement;
        currentPos = newPosition;
    }
}
