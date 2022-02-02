using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAttackerNode : Node
{
    private DefenderAI defender;

    public SearchAttackerNode(DefenderAI defender)
    {
        this.defender = defender;
    }

    public override NODE_STATE Evaluate()
    {
        defender.StopMove();

        AttackerAI attackerHoldingBall = ObjectLoader.INSTANCE.AttackerHoldingBall;
        if (attackerHoldingBall == null)
            return NODE_STATE.FAILED;

        float distance = (attackerHoldingBall.transform.position - defender.transform.position).sqrMagnitude;
        if(distance <= 17.64)
        {
            defender.IsAttackerFound = true;
            defender.TargetAttacker = attackerHoldingBall;
            nodeState = NODE_STATE.SUCCESS;
        }
        else
        {
            nodeState = NODE_STATE.FAILED;
        }

        return nodeState;
    }
}
