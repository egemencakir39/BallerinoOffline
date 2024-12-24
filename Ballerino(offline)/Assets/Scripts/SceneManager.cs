using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        FindObjectOfType<CardSelect>().SaveSelectedCards();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void mainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void loadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void player1CardSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Player1CardSelect");
    }

    public void player2CardSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Player2CardSelect");
    }
}
