using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    
    [Header("Text")]
    [SerializeField] private Text player1ScoreText;
    [SerializeField] private Text player2ScoreText;
    [SerializeField] private Text countDownText;
    [SerializeField] private Text timerText;

    private int player1Score = 0;
    private int player2Score = 0;

    [Header("References")]
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private Transform ball;


    [Header("Position")]
    [SerializeField] private Vector2 player1StartPosition;
    [SerializeField] private Vector2 player2StartPosition;
    [SerializeField] private Vector2 ballStartPosition;

    private Rigidbody2D ballRb;
    private Rigidbody2D player1Rb;
    private Rigidbody2D player2Rb;

    [Header("Timer")]
    [SerializeField] private float gameDuration = 300f;
    private float timer;


    private void Start()
    {
        UpdateScoreText();
        countDownText.gameObject.SetActive(false);
        ballRb = ball.GetComponent<Rigidbody2D>();
        player1Rb = player1.GetComponent<Rigidbody2D>();
        player2Rb = player2.GetComponent<Rigidbody2D>();
        player1.position = player1StartPosition;
        player2.position = player2StartPosition;
        ball.position = ballStartPosition;

        timer = gameDuration;
        UpdateTimerText();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;
            UpdateTimerText();

            if (timer <= 0)
            {
                timer = 0;
                EndGame();

            }
        }
    }
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndGame()
    {
        EnablePlayerMovement(false);
        ballRb.velocity = Vector2.zero;
        ballRb.angularVelocity = 0f;

        player1Rb.velocity = Vector2.zero;
        player1Rb.angularVelocity = 0f;

        player2Rb.velocity = Vector2.zero;
        player2Rb.angularVelocity = 0f;

        countDownText.gameObject.SetActive(true);
        if (player1Score > player2Score)
        {
            countDownText.text = "Player 1 Wins!";
        }
        else if (player2Score > player1Score)
        {
            countDownText.text = "Player 2 Wins";
        }
        else
        {
            countDownText.text = "It's a Scoreless";
        }
        StartCoroutine(HandleGameOver());
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void Player1Score()
    {
        player1Score++;
        UpdateScoreText();
        StartCoroutine(HandleGoalScored());
    }
    public void Player2Score()
    {
        player2Score++;
        UpdateScoreText();
        StartCoroutine(HandleGoalScored());
    }
    private void UpdateScoreText()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }
    private IEnumerator HandleGoalScored()
    {
        yield return new WaitForSeconds(2);
        EnablePlayerMovement(false);
        ResetPosition();
        countDownText.gameObject.SetActive(true);
        int countDown = 3;

        while (countDown > 0)
        {
            countDownText.text = countDown.ToString();
            yield return new WaitForSeconds(1);
            countDown--;
        }
        countDownText.gameObject.SetActive(false);

        EnablePlayerMovement(true);
    }
    private void ResetPosition()
    {
        player1.position = player1StartPosition;
        player2.position = player2StartPosition;
        ball.position = ballStartPosition;

        ballRb.velocity = Vector2.zero;
        ballRb.angularVelocity = 0f;

        player1Rb.velocity = Vector2.zero;
        player1Rb.angularVelocity = 0f;

        player2Rb.velocity = Vector2.zero;
        player2Rb.angularVelocity = 0f;
    }
    private void EnablePlayerMovement(bool enable)
    {
        player1.GetComponent<PlayerControl>().enabled = enable;
        player2.GetComponent<PlayerControl>().enabled = enable;
    }
}


