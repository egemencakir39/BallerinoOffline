using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Variable")]
    public float moveSpeed = 5f;
    [SerializeField] private float drag = 0.95f;
    public float shootPower = 10f;
    [SerializeField] private float shootRadius;
    [SerializeField] private string horizontalInput = "Horizontal";
    [SerializeField] private string verticalInput = "Vertical";
    [SerializeField] private KeyCode shootKey = KeyCode.Space;


    private Vector2 movement;
    public Rigidbody2D rb;
    public GameObject ball;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.FindWithTag("Ball");
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement);
        rb.velocity *= drag;
    }

    private void Update()
    {
        
        Movement();
        if (Input.GetKeyDown(shootKey))
        {
            ShootBall();
        }
    }

    public void Movement()
    {
        float moveX = Input.GetAxisRaw(horizontalInput);
        float moveY = Input.GetAxisRaw(verticalInput);

        Vector2 input = new Vector2(moveX, moveY);

        if (input.magnitude > 1)
        {
            input.Normalize();
        }
        movement = input * moveSpeed;
    }

    public void ShootBall()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, shootRadius);
        foreach (Collider2D coll in colls) {
            if (coll.gameObject == ball)
            {
                Vector2 shootDir = (ball.transform.position - transform.position).normalized;
                Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
                ballRb.AddForce(shootDir * shootPower, ForceMode2D.Impulse);
                Debug.Log("vurdu");
                break;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}









