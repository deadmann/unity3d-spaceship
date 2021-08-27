using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int _ships;
    private Vector2 _startPosition;
    private Quaternion _startRotation;

    public int maxShips = 3;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        _ships = maxShips;
        
        _startPosition = player.transform.position;
        _startRotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeNewShip()
    {
        if (_ships > 0)
        {
            _ships--;

            var newPlayer = Instantiate(player, _startPosition - (Vector2.up * (Screen.height/2f)), _startRotation);
            player = newPlayer;
            Debug.Log("Remained ships: " + _ships);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadSceneAsync(nameof(GameOver));
    }

    public static RectOffset GetBoarders()
    {
        if(Camera.main == null) return null;
        
        var screenStartBound = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        var screenEndBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        return new RectOffset((int)screenStartBound.x,(int) screenEndBound.x,(int) screenEndBound.y,(int) screenStartBound.y);
    }
}
