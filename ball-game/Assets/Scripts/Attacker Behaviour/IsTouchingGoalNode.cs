using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingGoalNode : Node
{
    private AttackerAI attackerAI;

    public IsTouchingGoalNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (attackerAI.IsCollidingWithGoal)
            nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
