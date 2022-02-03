using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerWinNode : Node
{
    SoldierAI soldier;

    public AttackerWinNode(SoldierAI soldier)
    {
        this.soldier = soldier;
    }

    public override NODE_STATE Evaluate()
    {
        GameManager.INSTANCE.RoundClear(soldier.Player);
        Debug.Log("Attacker win");
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
