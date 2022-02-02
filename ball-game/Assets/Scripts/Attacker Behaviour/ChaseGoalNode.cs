using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGoalNode : Node
{
    private AttackerAI attackerAI;

    public ChaseGoalNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        attackerAI.ChangeMoveTarget(ObjectLoader.INSTANCE.GetGoal(attackerAI.Player));
        attackerAI.Move(0.75f);
        nodeState = NODE_STATE.RUNNING;
        return nodeState;
    }
}
