using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInactiveNode : Node
{
    private SoldierAI soldierAI;

    public IsInactiveNode(SoldierAI soldierAI)
    {
        this.soldierAI = soldierAI;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (soldierAI.StillInactive())
        {
            nodeState = NODE_STATE.SUCCESS;
        }

        return nodeState;
    }
}
