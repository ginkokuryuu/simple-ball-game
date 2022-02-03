using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerLoseNode : Node
{
    SoldierAI soldier;

    public AttackerLoseNode(SoldierAI soldier)
    {
        this.soldier = soldier;
    }

    public override NODE_STATE Evaluate()
    {
        Owner winner = soldier.Player == Owner.Player1 ? Owner.Player2 : Owner.Player1;
        GameManager.INSTANCE.RoundClear(winner);
        Debug.Log("Attacker lose");
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
