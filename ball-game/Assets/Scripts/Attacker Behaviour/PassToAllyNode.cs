using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassToAllyNode : Node
{
    private AttackerAI attackerAI;

    public PassToAllyNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        attackerAI.PassTheBall();
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
