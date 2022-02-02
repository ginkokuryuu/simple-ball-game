using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerWinNode : Node
{
    public override NODE_STATE Evaluate()
    {
        //GameManager.INSTANCE.AttackerWin();
        Debug.Log("Attacker win");
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
