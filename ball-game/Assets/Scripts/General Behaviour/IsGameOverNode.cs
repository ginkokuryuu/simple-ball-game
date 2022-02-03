using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGameOverNode : Node
{
    private SoldierAI soldier;

    public IsGameOverNode(SoldierAI soldier)
    {
        this.soldier = soldier;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if(GameManager.INSTANCE.GameState != GameState.Running)
        {
            nodeState = NODE_STATE.SUCCESS;
            soldier.StopMove();
        }

        return nodeState;
    }
}
