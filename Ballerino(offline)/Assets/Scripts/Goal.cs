using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;
    private Manager gameManager;
   
    void Start()
    {
        gameManager = FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (isPlayer1Goal)
            {
                gameManager.Player2Score();
            }
            else
            {
                gameManager.Player1Score();
            }
        }
    }
}
