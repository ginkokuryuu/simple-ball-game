using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasReachedOriginNode : Node
{
    private DefenderAI defender;

    public HasReachedOriginNode(DefenderAI defender)
    {
        this.defender = defender;
    }

    public override NODE_STATE Evaluate()
    {
        nodeState = NODE_STATE.FAILED;
        float distance = (defender.OriginPos - defender.transform.position).sqrMagnitude;
        if(distance < 0.1f)
        {
            defender.StopMove();
            nodeState = NODE_STATE.SUCCESS;
        }
        return nodeState;
    }
}
