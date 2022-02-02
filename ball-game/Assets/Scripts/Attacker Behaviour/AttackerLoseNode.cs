using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerLoseNode : Node
{
    public override NODE_STATE Evaluate()
    {
        //GameManager.INSTANCE.AttackerLost();
        Debug.Log("Attacker lose");
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
