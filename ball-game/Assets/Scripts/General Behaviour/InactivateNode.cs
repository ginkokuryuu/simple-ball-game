using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactivateNode : Node
{
    private SoldierAI soldierAI;

    public InactivateNode(SoldierAI soldierAI)
    {
        this.soldierAI = soldierAI;
    }

    public override NODE_STATE Evaluate()
    {
        soldierAI.Inactivate();
        nodeState = NODE_STATE.SUCCESS;
        Debug.Log("inactivate");
        return nodeState;
    }
}
