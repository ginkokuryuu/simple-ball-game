using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttackerFoundNode : Node
{
    private DefenderAI defender;

    public IsAttackerFoundNode(DefenderAI defender)
    {
        this.defender = defender;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (defender.IsAttackerFound)
        {
            nodeState = NODE_STATE.SUCCESS;
            if (!defender.TargetAttacker.IsHoldingBall)
                nodeState = NODE_STATE.FAILED;
        }
        return nodeState;
    }
}
