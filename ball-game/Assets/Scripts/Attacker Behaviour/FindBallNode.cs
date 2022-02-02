using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBallNode : Node
{
    private AttackerAI attackerAI;

    public FindBallNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        Ball ball = ObjectLoader.INSTANCE.Ball;

        if (ball == null)
            return NODE_STATE.FAILED;

        nodeState = NODE_STATE.SUCCESS;
        if (ball.IsBeingHeld)
            nodeState = NODE_STATE.FAILED;

        return nodeState;
    }
}
