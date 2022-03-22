using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAGameManager : MonoBehaviour
{
    public int stageNumber = 1;
    public int playerLives = 3;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerLives == 0)
        {
            Debug.Log("The Game is Over!");
            gameOver = true;
        }
    }
}
