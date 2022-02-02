using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingBallNode : Node
{
    private AttackerAI attackerAI;

    public IsTouchingBallNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (attackerAI.IsCollidingWithBall)
            nodeState = NODE_STATE.SUCCESS;

        return nodeState;
    }
}
