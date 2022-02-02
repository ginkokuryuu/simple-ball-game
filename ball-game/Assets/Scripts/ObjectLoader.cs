using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectLoader : MonoBehaviour
{
    public static ObjectLoader INSTANCE;

    public List<AttackerAI> AllAttackers = new List<AttackerAI>();
    public Ball Ball;
    public AttackerAI AttackerHoldingBall;
    [SerializeField] private List<Transform> goals = new List<Transform>();

    private void Awake()
    {
        INSTANCE = this;
    }

    public Transform GetGoal(Owner player)
    {
        switch (player)
        {
            case Owner.Player1:
                return goals[1];
            case Owner.Player2:
                return goals[0];
            default:
                return goals[0];
        }
    }
}