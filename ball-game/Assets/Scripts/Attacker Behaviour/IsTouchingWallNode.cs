using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingWallNode : Node
{
    private AttackerAI attackerAI;

    public IsTouchingWallNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (attackerAI.IsCollidingWithWall)
            nodeState = NODE_STATE.SUCCESS;

        return nodeState;
    }
}
