using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public Text scoreTextP1; 
    public Text scoreTextP2;

    public PlayerController player1;
    public PlayerController player2;

    int score = 0;
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
            score += amount;
            scoreTextP1.text = "Player1 Score: " + score.ToString();
        }

        if (player.name == player2.name)
        {
            score += amount;
            scoreTextP2.text = "Player2 Score: " + score.ToString();
        }
    }
}
