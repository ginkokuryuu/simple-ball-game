using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const
{
    public const float scaleMultiplier = 1f;
}

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
    
    int currentMatch = 0;
    GameState gameState = GameState.WaitingToStart;
    int currentScore = 0;

    public int CurrentMatch { get => currentMatch; set => currentMatch = value; }
    public GameState GameState { get => gameState; set => gameState = value; }
    public int CurrentScore { get => currentScore; set => currentScore = value; }

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

    }

    public void TryStartGame()
    {
        if (gameState == GameState.WaitingToStart)
            StartGame();
        else if (gameState == GameState.WaitingToNextRound)
            NextRound();
        else if (gameState == GameState.WaitingMaze)
            MazeStart();
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

    public void MazeStart()
    {
        MazeHandler.INSTANCE.StartMaze();
        MyEventSystem.INSTANCE.MazeStart();

        gameState = GameState.MazeRunning;
    }

    public void GameOver()
    {
        gameState = GameState.WaitingToStart;

        MyEventSystem.INSTANCE.GameOver();
    }

    public void MazeClear(bool _win)
    {
        if (_win)
            currentScore += 1;
        else
            currentScore -= 1;

        GameOver();
        MenuHandler.INSTANCE.GameOver(false);
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
            bool needPenalty = (currentScore == 0);
            if (needPenalty)
            {
                gameState = GameState.WaitingMaze;
                MenuHandler.INSTANCE.GameOver(true);
            }
            else
            {
                GameOver();
                MenuHandler.INSTANCE.GameOver(false);
            }
        }
        else
        {
            MenuHandler.INSTANCE.RoundEnd(_winner);
        }
    }
}
