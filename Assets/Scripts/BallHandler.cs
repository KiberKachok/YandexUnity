using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class BallHandler : MonoBehaviour
{
    public float liftForce;
    public TextMeshProUGUI tipText;
    public TextMeshProUGUI scoreText;
    public UnityEvent OnGameStartedAction;

    private bool isGameStarted = false;
    private float score;
    private Rigidbody2D _rigidbody;
    private ParticleSystem _particleSystem;


    void Start()
    {
        score = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //Обнаружение начала игры
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && !isGameStarted)
        {
            isGameStarted = true;
            OnGameStartedAction.Invoke();
        }
    }

    private void FixedUpdate()
    {
        //Придание толчка шарику
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            _rigidbody.AddForce(Vector2.up * liftForce * Time.fixedDeltaTime);
    }

    public void OnGameStarted()
    {
        tipText.enabled = false;
        _rigidbody.simulated = true;
        _particleSystem.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.tag == "Reward")
        {
            score += 1;
            scoreText.text = score.ToString();
        }
    }
}
