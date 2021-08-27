using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemySpawnController : MonoBehaviour
{
    private Random _random;
    private float _timeStamp = 0;
    
    public List<GameObject> obstacles;

    public float cooldown = 4;
    public float sideMarginPercent = 5;
    public float baseGravityScaleMin = 0.01f;
    public float baseGravityScaleMax = 0.05f;

    private void Start()
    {
        _random = new Random();
        _random.InitState();
    }

    private void Update()
    {
        if (!(_timeStamp <= Time.time)) 
            return;
        
        var obstacle = CreateRandomObstacle();
        if(obstacle == null)
            return;

        obstacle.GetComponent<Rigidbody2D>().gravityScale = _random.NextFloat(baseGravityScaleMin, baseGravityScaleMax);
        
        AddObstacleComponents(obstacle);

        PositionObject(obstacle);
            
        _timeStamp = Time.time + cooldown;
    }

    private void AddObstacleComponents(GameObject obstacle)
    {
        obstacle.GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<BoxCollider2D>();
        //obstacle.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
    }

    private void PositionObject(GameObject obstacle)
    {
        var screenBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        var sideMargin = screenBound.x * 2 / 100 * sideMarginPercent;

        var x = _random.NextFloat(-screenBound.x + sideMargin, +screenBound.x - sideMargin);
        var y = screenBound.y * 2;
        
        obstacle.transform.position = new Vector2(x, y);
    }

    private GameObject CreateRandomObstacle()
    {
        var count = obstacles.Count;
        if (count == 0)
            return null;
        
        var selectedObstacle = _random.NextInt(count);

        return Instantiate(obstacles[selectedObstacle]);
    }
}
