using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBallNode : Node
{
    private AttackerAI attackerAI;

    public ChaseBallNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        attackerAI.ChangeMoveTarget(ObjectLoader.INSTANCE.Ball.transform);
        attackerAI.Move(1.5f * Const.scaleMultiplier);
        nodeState = NODE_STATE.RUNNING;
        return nodeState;
    }
}
