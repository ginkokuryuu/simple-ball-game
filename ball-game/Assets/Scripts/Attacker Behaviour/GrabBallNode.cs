using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBallNode : Node
{
    private AttackerAI attackerAI;

    public GrabBallNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        attackerAI.GrabBall();
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
