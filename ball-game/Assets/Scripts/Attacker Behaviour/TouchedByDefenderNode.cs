using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchedByDefenderNode : Node
{
    private AttackerAI attackerAI;

    public TouchedByDefenderNode(AttackerAI soldierAI)
    {
        this.attackerAI = soldierAI;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (attackerAI.IsCollidingWithDefender)
            nodeState = NODE_STATE.SUCCESS;

        return nodeState;
    }
}
