using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOriginNode : Node
{
    private DefenderAI defender;

    public ReturnToOriginNode(DefenderAI defender)
    {
        this.defender = defender;
    }

    public override NODE_STATE Evaluate()
    {
        defender.ChangeMoveTarget(null);
        defender.transform.LookAt(defender.OriginPos);
        defender.Move(2f * Const.scaleMultiplier);
        nodeState = NODE_STATE.RUNNING;

        return nodeState;
    }
}
