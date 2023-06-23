using KinematicCharacterController.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Win
}

public class GameManager : MonoBehaviour
{
    public GameState _gameState = GameState.Playing;
    public ExamplePlayer playerInput;
    public static GameManager Instance { get; private set; }

    public static Action OnStateLoseCondition;
    public static Action OnStateWinCondition;

    private void OnEnable()
    {
        OnStateWinCondition += WinCondition;
        OnStateLoseCondition += LoseCondition;
    }

    private void OnDisable()
    {
        OnStateWinCondition -= WinCondition;
        OnStateLoseCondition -= LoseCondition; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseCondition()
    {

    }

    public void WinCondition()
    {

    }
}
