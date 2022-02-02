using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtAttackerNode : Node
{
    private DefenderAI defender;

    public CaughtAttackerNode(DefenderAI defender)
    {
        this.defender = defender;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (defender.IsCollidingAttacker)
        {
            nodeState = NODE_STATE.SUCCESS;
            defender.IsChasingAttacker = false;
            defender.IsAttackerFound = false;
            defender.TargetAttacker = null;
        }

        return nodeState;
    }
}
