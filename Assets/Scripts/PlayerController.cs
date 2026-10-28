using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{
    public UIDocument uIDocument;
    public GameObject boosterFlame;

    private Label scoreText;
    public float moveSpeed = 7f;
    public float maxSpeed = 15f;
    public bool godMode = false;
    private float score = 0f;
    public float scoreMultiplier = 10f;
    private float elapsedTime;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
    }

    void Update()
    {
        UpdateScore();
        MovePlayer();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //zniszczenie statku gracza w przypadku kolizji(jeśli nie ma włączonego God Mode'a)
        if(godMode == false)
        {
            if (collision.gameObject.CompareTag("Respawn"))
            {
                Destroy(gameObject);
            }
        }
        
    }
    private void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;
    }
    private void MovePlayer()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            //obliczanie położenia myszy względem świata gry
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePosition - transform.position).normalized;
            //obrócenie gracza w kierunku myszy i nadanie mu pędu.
            transform.up = direction;
            rb.AddForce(direction * moveSpeed);
            if(rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }
}
