using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyEventSystem : MonoBehaviour
{
    public static MyEventSystem INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }

    public Action OnGameStart;
    public void GameStart()
    {
        Debug.Log("Broadcasted Game Start");
        OnGameStart?.Invoke();
    }

    public Action OnGameOver;
    public void GameOver()
    {
        Debug.Log("Broadcasted Game Over");
        OnGameOver?.Invoke();
    }

    public Action OnRoundStart;
    public void StartRound()
    {
        Debug.Log("Broadcasted Round Start");
        OnRoundStart?.Invoke();
    }

    public Action OnRoundEnd;
    public void RoundEnd()
    {
        Debug.Log("Broadcasted Round End");
        OnRoundEnd?.Invoke();
    }
}
