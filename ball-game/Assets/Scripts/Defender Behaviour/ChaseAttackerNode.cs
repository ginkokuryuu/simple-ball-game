using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAttackerNode : Node
{
    private DefenderAI defender;

    public ChaseAttackerNode(DefenderAI defender)
    {
        this.defender = defender;
    }

    public override NODE_STATE Evaluate()
    {
        defender.IsChasingAttacker = true;
        defender.ChangeMoveTarget(defender.TargetAttacker.transform);
        defender.Move(1.0f);
        nodeState = NODE_STATE.RUNNING;
        return nodeState;
    }
}
