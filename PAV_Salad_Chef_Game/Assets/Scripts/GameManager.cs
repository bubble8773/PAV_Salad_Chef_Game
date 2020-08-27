﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public Text scoreTextP1; 
    public Text scoreTextP2;

    public PlayerController player1;
    public PlayerController player2;

    public Transform ordersFinished; 
    public Transform ordersFailed; 

    int scoreP1 = 0;
    int scoreP2 = 0;

   public bool isGameOver = false;

    private void Awake()
    {
        _instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void ClampUIPositions(Vector3 position, Slider slider)
    {
        Vector3 clampedPos = Camera.main.WorldToScreenPoint(position);
        slider.transform.position = clampedPos;
    }
    public void UpdateScores( int amount, PlayerController player)
    {
        if (player.name == player1.name)
        {
            scoreP1 += amount;
            scoreTextP1.text = "Player1 Score: " + scoreP1.ToString();
        }

        if (player.name == player2.name)
        {
            scoreP2 += amount;
            scoreTextP2.text = "Player2 Score: " + scoreP2.ToString();
        }
    }

    void AddSpeed(PlayerController player, float amount)
    {
        player.speed += amount;
    }
    void AddTime(float amount) {

        gameObject.GetComponent<Timer>().waitingTime += amount;
    }

  
    // Pauses the game
    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    // Resumes the game
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // Restarts the game
    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
