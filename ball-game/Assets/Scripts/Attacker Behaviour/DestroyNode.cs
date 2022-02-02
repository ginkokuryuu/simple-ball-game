using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNode : Node
{
    private AttackerAI attackerAI;

    public DestroyNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        attackerAI.DestroySelf();
        ObjectLoader.INSTANCE.AllAttackers.Remove(attackerAI);
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
