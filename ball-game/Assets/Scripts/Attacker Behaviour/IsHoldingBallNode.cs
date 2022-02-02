using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHoldingBallNode : Node
{
    private AttackerAI attackerAI;

    public IsHoldingBallNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (attackerAI.IsHoldingBall)
            nodeState = NODE_STATE.SUCCESS;

        return nodeState;
    }
}
