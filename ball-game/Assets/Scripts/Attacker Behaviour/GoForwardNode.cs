using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForwardNode : Node
{
    private AttackerAI attackerAI;

    public GoForwardNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        attackerAI.GoStraight(1.5f * Const.scaleMultiplier);
        nodeState = NODE_STATE.RUNNING;
        return nodeState;
    }
}
