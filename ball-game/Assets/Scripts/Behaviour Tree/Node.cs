using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    protected NODE_STATE nodeState;
    protected NODE_STATE NodeState { get => nodeState; }

    public abstract NODE_STATE Evaluate();
}

public enum NODE_STATE
{
    RUNNING, SUCCESS, FAILED
}