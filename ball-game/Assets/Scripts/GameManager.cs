using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WaitingToStart,
    Running,
    WaitingToNextRound,
    WaitingMaze,
    MazeRunning
}

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    
    [SerializeField] int currentMatch = 0;
    GameState gameState = GameState.WaitingToStart;
    int currentScore = 0;

    public int CurrentMatch { get => currentMatch; set => currentMatch = value; }
    public GameState GameState { get => gameState; set => gameState = value; }

    private void Awake()
    {
        INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == GameState.WaitingToStart)
                StartGame();
            else if (gameState == GameState.WaitingToNextRound)
                NextRound();
        }
    }

    public void StartGame()
    {
        currentScore = 0;
        currentMatch = 0;

        MyEventSystem.INSTANCE.GameStart();
        MyEventSystem.INSTANCE.StartRound();

        gameState = GameState.Running;
    }

    public void NextRound()
    {
        MyEventSystem.INSTANCE.StartRound();

        gameState = GameState.Running;
    }

    public void GameOver()
    {
        gameState = GameState.WaitingToStart;

        MyEventSystem.INSTANCE.GameOver();
    }

    public void RoundClear(Owner _winner)
    {
        gameState = GameState.WaitingToNextRound;
        MyEventSystem.INSTANCE.RoundEnd();

        gameState = GameState.WaitingToNextRound;

        if(_winner == Owner.Player1)
        {
            currentScore += 1;
        }
        else if(_winner == Owner.Player2)
        {
            currentScore -= 1;
        }

        currentMatch += 1;
        if(currentMatch == 5)
        {
            GameOver();
        }
    }
}
