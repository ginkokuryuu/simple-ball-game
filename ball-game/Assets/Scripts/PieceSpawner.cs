﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public static PieceSpawner INSTANCE;

    [SerializeField] GameObject ballPrefabs = null;
    [SerializeField] Transform fieldPlay = null;

    [Header("Soldier Prefabs")]
    [SerializeField] GameObject player1AttackerPrefabs = null;
    [SerializeField] GameObject player1DefenderPrefabs = null;
    [SerializeField] GameObject player2AttackerPrefabs = null;
    [SerializeField] GameObject player2DefenderPrefabs = null;

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Start()
    {
        SpawnBall();
    }

    void SpawnBall()
    {
        bool evenMatch = GameManager.INSTANCE.CurrentMatch % 2 == 0;
        float minZ = evenMatch ? -11f : 1f;
        float maxZ = evenMatch ? -1f : 11f;
        float minX = -5f;
        float maxX = 5f;

        GameObject theBall = Instantiate(ballPrefabs, fieldPlay, false);
        theBall.transform.position = new Vector3(Random.Range(minX, maxX), 0.85f, Random.Range(minZ, maxZ));
    }

    public bool TryToSpawnSoldier(SoldierType _soldierType, Owner _owner, Vector3 _position)
    {
        bool result = false;
        GameObject toBeSpawned;

        if(_soldierType == SoldierType.Attacker && _owner == Owner.Player1)
            toBeSpawned = player1AttackerPrefabs;
        else if (_soldierType == SoldierType.Attacker && _owner == Owner.Player2)
            toBeSpawned = player2AttackerPrefabs;
        else if (_soldierType == SoldierType.Defender && _owner == Owner.Player1)
            toBeSpawned = player1DefenderPrefabs;
        else if (_soldierType == SoldierType.Defender && _owner == Owner.Player2)
            toBeSpawned = player2DefenderPrefabs;
        else
            return false;

        Instantiate(toBeSpawned, new Vector3(_position.x, 1.5f, _position.z), Quaternion.identity);
        result = true;
        return result;
    }
}