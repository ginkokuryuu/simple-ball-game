using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsChasingAttackerNode : Node
{
    private DefenderAI defender;

    public IsChasingAttackerNode(DefenderAI defender)
    {
        this.defender = defender;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        if (defender.IsChasingAttacker)
            nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
